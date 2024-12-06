using FlyNow.Data;
using FlyNow.DTOs;
using FlyNow.EfModels;
using FlyNow.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	public class CompanhiaAereaController : Base
	{
		public CompanhiaAereaController() : base(new FlyNowContext(), new ServicoLog()) { }
		//public CompanhiaAereaController(FlyNowContext db) : base(db, new ServicoLog()) { }

		[HttpPost("GerarValorBagagem")]
		public IActionResult gerarValorBagagem([FromQuery] ValorBagagemDto valorBagagemDto)
		{
			if (valorBagagemDto.ValorPrimeiraBagagem <= 0)
				return BadRequest("É obrigatório ter valor na primeira bagagem.");
			if (valorBagagemDto.ValorBagagemAdicional <= 0)
				return BadRequest("É obrigatório ter valor na bagagem adicional.");

			var companhiaExistente = db.Companhiaaereas.Any(c => c.Cod == valorBagagemDto.CompanhiaaereaCod);
			if (!companhiaExistente)
				return BadRequest("Não existe uma companhia aérea com esse código");

			var valorBagagem = new Valorbagagem
			{
				ValorPrimeiraBagagem = valorBagagemDto.ValorPrimeiraBagagem,
				ValorBagagemAdicional = valorBagagemDto.ValorBagagemAdicional,
				CompanhiaaereaCod = valorBagagemDto.CompanhiaaereaCod,
			};

			try
			{
				db.Valorbagagems.Add(valorBagagem);
				db.SaveChanges();
				return CreatedAtAction(nameof(gerarValorBagagem), new { id = valorBagagem.Id}, valorBagagem);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao gerar um Valor de Bagagem. {ex.Message}");
			}
		}

		[HttpPost("CadastrarVoo")]
		public IActionResult CadastrarVoo([FromQuery] VooDto vooDto)
		{
			if (string.IsNullOrEmpty(vooDto.CodVoo))
				return BadRequest("O código do voo é obrigatório.");

			if (vooDto.Data == default)
				return BadRequest("A data do voo é obrigatória.");

			if (vooDto.CompanhiaaereaCod == 0)
				return BadRequest("A companhia aérea é obrigatória.");

			if (vooDto.AeronaveIdAeronave == 0)
				return BadRequest("A aeronave é obrigatória.");

			if (vooDto.IdAeroportoOrigem == 0 || vooDto.IdAeroportoDestino == 0)
				return BadRequest("Os aeroportos de origem e destino são obrigatórios.");

			var companhia = db.Companhiaaereas.Find(vooDto.CompanhiaaereaCod);
			if (companhia == null)
				return NotFound("Companhia aérea não encontrada.");

			var aeronave = db.Aeronaves.Find(vooDto.AeronaveIdAeronave);
			if (aeronave == null)
				return NotFound("A aeronave não encontrada.");

			var aeroportoOrigem = db.Aeroportos.Find(vooDto.IdAeroportoOrigem);
			if (aeroportoOrigem == null)
				return NotFound("Aeroporto de origem não encontrado.");

			var aeroportoDestino = db.Aeroportos.Find(vooDto.IdAeroportoDestino);
			if (aeroportoDestino == null)
				return NotFound("Aeroporto de destino não encontrado.");

			var voo = new Voo
			{
				CodVoo = vooDto.CodVoo,
				Data = vooDto.Data,
				EhInternacional = vooDto.EhInternacional,
				Duracao = vooDto.Duracao,
				CompanhiaaereaCod = vooDto.CompanhiaaereaCod,
				AeronaveIdAeronave = vooDto.AeronaveIdAeronave,
				IdAeroportoOrigem = vooDto.IdAeroportoOrigem,
				IdAeroportoDestino = vooDto.IdAeroportoDestino
			};

			try
			{
				db.Voos.Add(voo);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarVoo), new { id = voo.IdVoo }, voo);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar voo: {ex.Message}");
			}
		}

		[HttpPost("CadastrarAeronave")]
		[HttpPost("CadastrarAeronave")]
		public IActionResult CadastrarAeronave([FromQuery] AeronaveDto aeronaveDto)
		{
			if (string.IsNullOrEmpty(aeronaveDto.Nome))
				return BadRequest("O nome da aeronave é obrigatório.");

			if (aeronaveDto.CapacidadePassageiros <= 0)
				return BadRequest("A capacidade de passageiros é obrigatória e deve ser maior que zero.");

			if (aeronaveDto.CapacidadeBagagens <= 0)
				return BadRequest("A capacidade de bagagens é obrigatória e deve ser maior que zero.");

			// Calculando a capacidade de passageiros se não for fornecido no DTO
			int capacidadeTotalAssentos = aeronaveDto.NumeroFileiras * aeronaveDto.AssentosPorFileira;
			if (capacidadeTotalAssentos != aeronaveDto.CapacidadePassageiros)
				return BadRequest("A capacidade total de assentos não corresponde à quantidade de fileiras e assentos por fileira.");

			var aeronave = new Aeronave
			{
				Nome = aeronaveDto.Nome,
				CapacidadePassageiros = aeronaveDto.CapacidadePassageiros,
				CapacidadeBagagens = aeronaveDto.CapacidadeBagagens,
				NumeroFileiras = aeronaveDto.NumeroFileiras,
				AssentosPorFileira = aeronaveDto.AssentosPorFileira
			};

			try
			{
				db.Aeronaves.Add(aeronave);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarAeronave), new { id = aeronave.IdAeronave }, aeronave);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar aeronave: {ex.Message}");
			}
		}

		[HttpPost("ProgramarVoos")]
		public IActionResult ProgramarVoos(int idVoo, string diasSemana)
		{
			// Busca o voo pelo ID
			var voo = db.Voos.FirstOrDefault(v => v.IdVoo == idVoo);
			if (voo == null)
				return BadRequest("Voo não encontrado.");

			// Verifica se a companhia aérea associada ao voo existe
			var companhia = db.Companhiaaereas.FirstOrDefault(c => c.Cod == voo.CompanhiaaereaCod);
			if (companhia == null)
				return BadRequest("A companhia aérea associada ao voo não existe.");

			// Verifica se a aeronave associada ao voo existe
			var aeronave = db.Aeronaves.FirstOrDefault(a => a.IdAeronave == voo.AeronaveIdAeronave);
			if (aeronave == null)
				return BadRequest("A aeronave associada ao voo não existe.");

			// Separa os dias da semana pela vírgula
			var diasSemanaArray = diasSemana.Split(',');

			// Define a data inicial (hoje) e a data limite (30 dias depois)
			var dataInicial = DateTime.Now;
			var dataLimite = dataInicial.AddDays(30);

			List<Voo> voosProgramados = new List<Voo>();

			// Loop para programar os voos para os dias da semana fornecidos
			for (var data = dataInicial; data <= dataLimite; data = data.AddDays(1))
			{
				// Verifica se o dia da semana atual está entre os dias fornecidos
				if (diasSemanaArray.Contains(data.DayOfWeek.ToString()))
				{
					// Criação do voo programado com a companhia aérea e aeronave associadas
					var vooProgramado = new Voo
					{
						CodVoo = voo.CodVoo,
						IdAeroportoOrigem = voo.IdAeroportoOrigem,
						IdAeroportoDestino = voo.IdAeroportoDestino,
						Data = data, // Usa a data calculada
						Duracao = voo.Duracao,
						CompanhiaaereaCod = voo.CompanhiaaereaCod, // Associa a companhia aérea
						AeronaveIdAeronave = voo.AeronaveIdAeronave, // Associa a aeronave
					};

					// Adiciona o voo programado à lista
					voosProgramados.Add(vooProgramado);
				}
			}

			// Se não houver voos programados, retorna uma mensagem
			if (voosProgramados.Count == 0)
				return BadRequest("Nenhum voo foi programado para os dias fornecidos.");

			try
			{
				// Adiciona os voos programados ao banco
				db.Voos.AddRange(voosProgramados);
				db.SaveChanges();

				// Retorna sucesso com o número de voos programados
				return Ok($"Voos programados com sucesso para o período de 30 dias. Total de voos programados: {voosProgramados.Count}");
			}
			catch (Exception ex)
			{
				// Retorna erro se houver falha ao salvar os voos no banco
				return BadRequest($"Erro ao programar voos: {ex.Message}");
			}
		}

		[HttpPost("CancelarVoo")]
		public IActionResult CancelarVoo(int vooId)
		{
			var voo = db.Voos.FirstOrDefault(v => v.IdVoo == vooId);
			if (voo == null)
				return BadRequest("Voo não encontrado.");

			var passagensAssociadas = db.Passagems
					.Where(p => p.IdVoo1 == vooId || p.IdVoo2 == vooId)
					.ToList();

			foreach (var passagem in passagensAssociadas)
			{
				passagem.Status = StatusPassagemDto.Cancelada;

				var bilhetes = db.Bilhetes.Where(b => b.PassagemIdPassagem == passagem.IdPassagem).ToList();
				foreach (var bilhete in bilhetes)
				{
					if (bilhete.IdAssento.HasValue)
					{
						var assento = db.Assentos.FirstOrDefault(a => a.IdAssento == bilhete.IdAssento.Value);
						if (assento != null)
						{
							assento.Ocupado = 0;
						}
					}
				}
			}

			try
			{
				db.SaveChanges();
				return Ok("Voo cancelado e passagens associadas alteradas.");
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cancelar voo: {ex.Message}");
			}
		}

	}
}


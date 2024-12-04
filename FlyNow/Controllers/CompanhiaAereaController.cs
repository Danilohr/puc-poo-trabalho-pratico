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
		public IActionResult CadastrarAeronave([FromQuery] AeronaveDto aeronaveDto)
		{
			if (string.IsNullOrEmpty(aeronaveDto.Nome))
				return BadRequest("O nome da aeronave é obrigatório.");

			if (aeronaveDto.CapacidadePassageiros <= 0)
				return BadRequest("A capacidade de passageiros é obrigatória e deve ser maior que zero.");

			if (aeronaveDto.CapacidadeBagagens <= 0)
				return BadRequest("A capacidade de bagagens é obrigatória e deve ser maior que zero.");

			var aeronave = new Aeronave
			{
				Nome = aeronaveDto.Nome,
				CapacidadePassageiros = aeronaveDto.CapacidadePassageiros,
				CapacidadeBagagens = aeronaveDto.CapacidadeBagagens
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
	}
}

using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using FlyNow.Interfaces;
using FlyNow.Services;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VooController : Base
	{
		public VooController() : base (new FlyNowContext(), new ServicoLog()) {}
		public VooController(FlyNowContext db) : base (db, new ServicoLog()) { }

				private float CalculaDistanciaKm(float lat1, float long1, float lat2, float long2)
		{
			return 110.57f * MathF.Sqrt(MathF.Pow(lat2 - lat1, 2) + MathF.Pow(long2 - long1, 2));
		}

		private void AtualizarDuracaoEHorario(Voo voo)
		{
			var aeroportoOrigem = db.Aeroportos.Find(voo.IdAeroportoOrigem);
			var aeroportoDestino = db.Aeroportos.Find(voo.IdAeroportoDestino);

			if (aeroportoOrigem == null || aeroportoDestino == null)
				throw new Exception("Aeroporto de origem ou destino não encontrado.");

			float distanciaKm = CalculaDistanciaKm(
					aeroportoOrigem.Latitude,
					aeroportoOrigem.Longitude,
					aeroportoDestino.Latitude,
					aeroportoDestino.Longitude
			);

			if (voo.VelocidadeMedia <= 0)
				throw new Exception("Velocidade média da aeronave deve ser maior que zero.");

			float duracaoHoras = distanciaKm / voo.VelocidadeMedia;

			voo.Duracao = TimeOnly.FromTimeSpan(TimeSpan.FromHours(duracaoHoras));
			voo.HorarioPrevistoChegada = voo.Data.AddHours(duracaoHoras);
		}

		[HttpGet]
		public IActionResult Get()
		{
			List<Voo> lista = db.Voos.ToList();


			return Ok(lista);
		}

		[HttpPost]
		public IActionResult Post(Voo voo)
		{
			db.Voos.Add(voo);

			try
			{
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
			logServico.RegistrarLog($"Novo voo criado: Código {voo.CodVoo}, Data {voo.Data}.");
			return Ok("Voo criado com sucesso.");
		}

		[HttpGet("GetHistorico")]
		public IActionResult GetHistorico(int idPassageiro)
		{
			var bilhetesComVoos = db.Bilhetes
				.Where(b => b.PassageiroIdPassageiro == idPassageiro)
				.Select(b => new
				{
					Voo1 = b.PassagemIdPassagemNavigation.Voo1,
					Voo2 = b.PassagemIdPassagemNavigation.Voo2
				})
				.ToList();

			// Combina ambos os voos em uma lista única
			var listaDeVoos = bilhetesComVoos
					.SelectMany(b => new[] { b.Voo1, b.Voo2 })
					.Where(v => v != null) // Remove voos nulos
					.OrderBy(v => v.Data) // Ordena por data do voo
					.ToList();

			logServico.RegistrarLog($"Consulta de histórico de voos para passageiro {idPassageiro}.");
			return Ok(listaDeVoos);
		}

		[HttpGet("getVooInternacional")]
		public IActionResult GetVoosInternacionais()
		{
			var lista = db.Voos.Where(i => i.EhInternacional == 1);

			logServico.RegistrarLog("Consulta de voos internacionais realizada.");
			return Ok(lista);
		}

		[HttpGet("buscar-voos")]
		public IActionResult GetVoos([FromQuery] int idAeroOrigem, [FromQuery] int idAeroDestino, [FromQuery] DateTime dataIda, [FromQuery] DateTime? dataVolta = null)
		{
			var voosIda = db.Voos
					.Where(v => v.IdAeroportoOrigem == idAeroOrigem &&
											v.IdAeroportoDestino == idAeroDestino &&
											v.Data.Date == dataIda.Date)
					.ToList();

			var voosIdaComConexao = db.Voos
				.Where(v1 => v1.IdAeroportoOrigem == idAeroOrigem &&
										 v1.Data.Date == dataIda.Date)
				.SelectMany(v1 => db.Voos
						.Where(v2 => v2.IdAeroportoOrigem == v1.IdAeroportoDestino &&
												 v2.IdAeroportoDestino == idAeroDestino &&
												 v2.Data.Date == dataIda.Date)
						.Select(v2 => new
						{
							Voo1 = v1,
							Voo2 = v2
						}))
				.ToList();

			var resultado = new List<object>();

			resultado.AddRange(voosIda.Select(v => new
			{
				Tipo = "Direto",
				Voo = v,
				Conexao = null as object
			}));

			resultado.AddRange(voosIdaComConexao.Select(c => new
			{
				Tipo = "Conexão",
				Voo = c.Voo1,
				Conexao = c.Voo2
			}));

			if (dataVolta.HasValue)
			{
				var voosVolta = db.Voos
						.Where(v => v.IdAeroportoOrigem == idAeroDestino &&
												v.IdAeroportoDestino == idAeroOrigem &&
												v.Data.Date == dataVolta.Value.Date)
						.ToList();

				var voosVoltaComConexao = db.Voos
						.Where(v1 => v1.IdAeroportoOrigem == idAeroDestino &&
												 v1.Data.Date == dataVolta.Value.Date)
						.SelectMany(v1 => db.Voos
								.Where(v2 => v2.IdAeroportoOrigem == v1.IdAeroportoDestino &&
														 v2.IdAeroportoDestino == idAeroOrigem &&
														 v2.Data.Date == dataVolta.Value.Date)
								.Select(v2 => new
								{
									Voo1 = v1,
									Voo2 = v2
								}))
						.ToList();

				resultado.AddRange(voosVolta.Select(v => new
				{
					Tipo = "Retorno Direto",
					Voo = v,
					Conexao = null as object
				}));

				resultado.AddRange(voosVoltaComConexao.Select(c => new
				{
					Tipo = "Retorno com Conexão",
					Voo = c.Voo1,
					Conexao = c.Voo2
				}));
			}

			return Ok(resultado);
		}

		[HttpPost]
		public IActionResult SaveVoo(Voo voo)
		{
			try
			{
				AtualizarDuracaoEHorario(voo); // Calcula distância e horários
				db.Voos.Add(voo);
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao salvar voo: {ex.Message}");
			}
    }
	}

}

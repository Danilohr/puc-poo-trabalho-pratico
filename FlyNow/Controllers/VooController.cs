using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using FlyNow.Interfaces;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VooController : Base
	{
		private readonly ILog _logServico;

		public VooController(FlyNowContext context, ILog logServico) : base(context) 
		{
			_logServico = logServico;
		}
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
			_logServico.RegistrarLog($"Novo voo criado: Código {voo.CodVoo}, Data {voo.Data}.");
			return Ok("Voo criado com sucesso.");
		}

		[HttpGet("GetHistorico")]
		public IActionResult GetHistorico(int idPassageiro)
		{
			var bilhetesComVoos = db.Bilhetes
				.Where(b => b.PassageiroIdPassageiro == idPassageiro)
				.Select(b => new
				{
					Voo1 = b.Passagem.Voo1,
					Voo2 = b.Passagem.Voo2
				})
				.ToList();

			// Combina ambos os voos em uma lista única
			var listaDeVoos = bilhetesComVoos
					.SelectMany(b => new[] { b.Voo1, b.Voo2 })
					.Where(v => v != null) // Remove voos nulos
					.OrderBy(v => v.Data) // Ordena por data do voo
					.ToList();

			return Ok(listaDeVoos);
			_logServico.RegistrarLog($"Consulta de histórico de voos para passageiro {idPassageiro}.");
		}

		[HttpGet("getVooInternacional")]
		public IActionResult GetVoosInternacionais()
		{
			var lista = db.Voos.Where(i => i.EhInternacional == 1);

			_logServico.RegistrarLog("Consulta de voos internacionais realizada.");
			return Ok(lista);
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

			return Ok(voo);
		}


	}

}

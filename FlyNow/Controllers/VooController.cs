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


		[HttpGet("buscar-voo")]
		public IActionResult GetVoo([FromQuery] int idAeroOrigem, [FromQuery] int idAeroDestino, [FromQuery] DateTime data)
		{
			var voo = db.Voos
				.Where(v => v.IdAeroportoOrigem == idAeroOrigem && v.IdAeroportoDestino == idAeroDestino && v.Data == data)
				.ToList();

			return Ok(voo);
		}
	}

}
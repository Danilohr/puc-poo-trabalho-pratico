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

		public VooController(FlyNowContext context, ILog logServico) : base(context) // Passa o contexto para a base
		{
			_logServico = logServico; // Inicia o serviço de log
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
	}

}
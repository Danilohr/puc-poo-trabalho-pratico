using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VooController : Base
	{

		public VooController(FlyNowContext context) : base(context)
		{
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
			catch (Exception ex) {
				return BadRequest(ex.Message);
			}

			return Ok();
		}

		[HttpGet("GetHistorico")]
		public IActionResult GetHistorico(int idPassageiro)
		{
			var historicoVoos = db.Bilhetes
														.Where(b => b.PassageiroIdPassageiro == idPassageiro)
														.Include(b => b.PassagemIdPassagemNavigation)
														.ThenInclude(p => p.IdVoo1Navigation) // Inclui o voo principal relacionado
														.Include(b => b.PassagemIdPassagemNavigation.IdVoo2Navigation) // Inclui o voo de conexão, caso exista
														.Select(b => new {
															PassageiroId = b.PassageiroIdPassageiro,
															Voos = new List<Voo> { b.PassagemIdPassagemNavigation.IdVoo1Navigation}.ToList() // Lista de voos não nulos
														})
														.ToList();

			return Ok(historicoVoos);
		}

		[HttpGet("getVooInternacional")]
		public IActionResult GetVoosInternacionais()
		{
			var lista = db.Voos.Where(i => i.EhInternacional == 1);

			return Ok(lista);
		}


	}
}

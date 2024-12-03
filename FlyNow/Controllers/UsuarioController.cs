using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography.Xml;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuarioController : Base
	{
		public UsuarioController(FlyNowContext context) : base(context)
		{
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

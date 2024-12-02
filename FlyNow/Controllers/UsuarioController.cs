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

		[HttpGet("Logar")]
		public IActionResult Logar([FromQuery] string login, [FromQuery] string senha)
		{
			if (string.IsNullOrEmpty(login))
				return BadRequest("O login é obrigatório.");
			if (string.IsNullOrEmpty(senha))
				return BadRequest("A senha é obrigatória.");

			var usuarioExistente = db.Usuarios.SingleOrDefault(u => u.Login == login);
			if (usuarioExistente == null)
				return Unauthorized("Usuário não encontrado.");

			if (usuarioExistente.Senha != senha)
				return Unauthorized("Senha incorreta.");

			return Ok(usuarioExistente);
		}

	}
}

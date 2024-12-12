using FlyNow.Data;
using FlyNow.EfModels;
using FlyNow.Interfaces;
using FlyNow.Services;
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
		public UsuarioController() : base(new FlyNowContext(), new ServicoLog()) { }
		public UsuarioController(FlyNowContext db) : base(db, new ServicoLog()) { }


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

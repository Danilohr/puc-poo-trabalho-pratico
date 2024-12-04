using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using FlyNow.Interfaces;
using FlyNow.Services;
using FlyNow.DTOs;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PassageiroController : Base
	{
		public PassageiroController() : base (new FlyNowContext(), new ServicoLog()) {}
		public PassageiroController(FlyNowContext db) : base (db, new ServicoLog()) { }

		[HttpGet("GetPassageirosVip/{idCompanhia}")]
		public IActionResult GetPassageirosVip(int idCompanhia)
		{
			// Busca todos os passageiros VIP de uma companhia aérea específica
			var passageirosVip = (from pv in db.PassageiroVIPs
														join p in db.Passageiros on pv.PassageiroIdPassageiro equals p.IdPassageiro
														where pv.CompanhiaaereaCod == idCompanhia
														select new
														{
															p.IdPassageiro,
															p.Nome,
															p.Cpf,
															p.Rg,
															p.Email
														}).ToList();

			if (!passageirosVip.Any())
			{
				return NotFound("Nenhum passageiro VIP encontrado para esta companhia aérea.");
			}

			logServico.RegistrarLog($"Consulta de passageiros VIP para a companhia {idCompanhia}.");
			return Ok(passageirosVip);
		}

		[HttpPost("TornarPassageiroVip")]
		public IActionResult TornarPassageiroVip(int idPassageiro, int idCompanhia)
		{
			// Verifica se o passageiro já é VIP para a companhia aérea especificada
			var passageiroVipExistente = db.PassageiroVIPs
				.FirstOrDefault(pv => pv.PassageiroIdPassageiro == idPassageiro && pv.CompanhiaaereaCod == idCompanhia);

			if (passageiroVipExistente != null)
			{
				return BadRequest("Este passageiro já é VIP para a companhia aérea especificada.");
			}

			// Cria um novo registro de PassageiroVIP
			var novoPassageiroVip = new PassageiroVip
			{
				PassageiroIdPassageiro = idPassageiro,
				CompanhiaaereaCod = idCompanhia
			};

			db.PassageiroVIPs.Add(novoPassageiroVip);

			try
			{
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao tornar passageiro VIP: {ex.Message}");
			}

			logServico.RegistrarLog($"Passageiro com ID {idPassageiro} tornado VIP na companhia {idCompanhia}.");

			return Ok("Passageiro tornado VIP com sucesso.");
		}

		[HttpPost("CadastrarPssageiro")]
		public IActionResult cadastrarPassageiro([FromQuery] PassageiroDto passageiroDto)
		{
			if (string.IsNullOrEmpty(passageiroDto.Nome))
				return BadRequest("Nome é obrigatório.");
			if (string.IsNullOrEmpty(passageiroDto.Cpf))
				return BadRequest("Cpf é obrigatório.");
			if (string.IsNullOrEmpty(passageiroDto.Email))
				return BadRequest("Email é obrigatório.");
			if (string.IsNullOrEmpty(passageiroDto.Rg))
				return BadRequest("Rg é obrigatório.");

			var usuarioExistente = db.Usuarios.Any(u => u.IdUsuario == passageiroDto.UsuarioIdUsuario);
			if (!usuarioExistente)
				return BadRequest("Id de usuário inválido.");

			var passageiro = new Passageiro
			{
				Nome = passageiroDto.Nome,
				Cpf = passageiroDto.Cpf,
				Email = passageiroDto.Email,
				Rg = passageiroDto.Rg,
				UsuarioIdUsuario = passageiroDto.UsuarioIdUsuario,
			};

			try
			{
				db.Passageiros.Add(passageiro);
				db.SaveChanges();
				return CreatedAtAction(nameof(cadastrarPassageiro), new { id = passageiro.IdPassageiro }, passageiro);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar passageiro: {ex.Message}");
			}
		}
	}
}
using FlyNow.Data;
using FlyNow.DTOs;
using FlyNow.EfModels;
using FlyNow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class FuncionarioController : Base
	{
		private readonly ServicoPassagem passagemService;

		public FuncionarioController() : base(new FlyNowContext(), new ServicoLog()) { passagemService = new ServicoPassagem(); }

		[HttpPost("CadastrarFuncionario")]
		public IActionResult CadastrarFuncionario([FromQuery] FuncionarioDto funcionarioDto)
		{
			if (string.IsNullOrEmpty(funcionarioDto.Nome))
				return BadRequest("O nome é obrigatório.");
			if (string.IsNullOrEmpty(funcionarioDto.Cpf))
				return BadRequest("O CPF é obrigatório.");
			if (string.IsNullOrEmpty(funcionarioDto.Email))
				return BadRequest("O e-mail é obrigatório.");

			var cpfExistente = db.Funcionarios.Any(f => f.Cpf == funcionarioDto.Cpf);
			if (cpfExistente)
				return Conflict("Já existe um funcionário cadastrado com este CPF.");

			int novoId = 1;
			if (db.Funcionarios.Any())
			{
				novoId = db.Funcionarios.Max(f => f.IdFuncionario) + 1;
			}

			var funcionario = new Funcionario
			{
				IdFuncionario = novoId,
				Nome = funcionarioDto.Nome,
				Cpf = funcionarioDto.Cpf,
				Email = funcionarioDto.Email,
			};

			try
			{
				db.Funcionarios.Add(funcionario);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarFuncionario), new { id = funcionario.IdFuncionario }, funcionario);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar funcionário: {ex.Message}");
			}
		}

		[HttpPost("{idFuncionario}/CadastrarUsuario")]
		public IActionResult CadastrarUsuario(int idFuncionario, [FromQuery] UsuarioDto usuarioDto)
		{
			if (string.IsNullOrEmpty(usuarioDto.Login))
				return BadRequest("O login é obrigatório.");
			if (string.IsNullOrEmpty(usuarioDto.Senha))
				return BadRequest("A senha é obrigatória.");

			var funcionario = db.Funcionarios.Find(idFuncionario);
			if (funcionario == null)
				return NotFound("Funcionário não encontrado.");

			var loginExistente = db.Usuarios.Any(u => u.Login == usuarioDto.Login);
			if (loginExistente)
				return Conflict("Já existe um usuário com este login.");

			var usuarioExistente = db.Usuarios.Any(u => u.FuncionarioIdFuncionario == idFuncionario);
			if (usuarioExistente)
				return Conflict("Este funcionário ja possui um usuário associado.");

			int novoId = 1;
			if (db.Usuarios.Any())
			{
				novoId = db.Usuarios.Max(u => u.IdUsuario) + 1;
			}

			var usuario = new Usuario
			{
				IdUsuario = novoId,
				Login = usuarioDto.Login,
				Senha = usuarioDto.Senha,
				FuncionarioIdFuncionario = idFuncionario,
			};

			try
			{
				db.Usuarios.Add(usuario);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarUsuario), new { idFuncionario, idUsuario = usuario.IdUsuario }, usuario);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar usuário: {ex.Message}");
			}
		}

		[HttpPost("CadastrarCompanhiaAerea")]
		public IActionResult CadastrarCompanhia([FromQuery] CompanhiaAereaDto companhiaAereaDto)
		{
			if (string.IsNullOrEmpty(companhiaAereaDto.Nome))
				return BadRequest("Nome é obrigatório.");
			if (string.IsNullOrEmpty(companhiaAereaDto.RazaoSocial))
				return BadRequest("Razao Social é obrigatório.");
			if (string.IsNullOrEmpty(companhiaAereaDto.Cnpj))
				return BadRequest("CNPJ é obrigatório.");
			if (companhiaAereaDto.TaxaRemuneracao <= 0)
				return BadRequest("A taxa de remuneção não pode ser menor ou igual a 0.");

			var cnpjExistente = db.Companhiaaereas.Any(c => c.Cnpj == companhiaAereaDto.Cnpj);
			if (cnpjExistente)
				return Conflict("Já existe outra companhia aérea cadastrada com esse CNPJ.");

			var companhia = new CompanhiaAerea
			{
				Nome = companhiaAereaDto.Nome,
				RazaoSocial = companhiaAereaDto.RazaoSocial,
				Cnpj = companhiaAereaDto.Cnpj,
				TaxaRemuneracao = companhiaAereaDto.TaxaRemuneracao,
			};

			try
			{
				db.Companhiaaereas.Add(companhia);
				db.SaveChanges();
				return CreatedAtAction(nameof(CompanhiaAerea), new { Cod = companhia.Cod }, companhia);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar companhia aérea: {ex.Message}");
			}
		}

		[HttpPost("CadastrarAeroporto")]
		public IActionResult CadastrarAeroporto([FromQuery] AeroportoDto aeroportoDto)
		{
			if (string.IsNullOrEmpty(aeroportoDto.Cidade))
				return BadRequest("Cidade é obrigatório.");
			if (string.IsNullOrEmpty(aeroportoDto.Uf))
				return BadRequest("UF é obrigatório.");
			if (string.IsNullOrEmpty(aeroportoDto.Nome))
				return BadRequest("Nome é obrigatório.");
			if (string.IsNullOrEmpty(aeroportoDto.Sigla))
				return BadRequest("Sigla é obrigatório.");
			if (aeroportoDto.Latitude == 0 || aeroportoDto.Longitude == 0)
				return BadRequest("Latitude e Longitude são obrigatórios e devem ser diferentes de zero.");

			var siglaExistente = db.Aeroportos.Any(a => a.Sigla == aeroportoDto.Sigla);
			if (siglaExistente)
				return Conflict("Já existe um aeroporto cadastrado com essa sigla");

			var aeroporto = new Aeroporto
			{
				Nome = aeroportoDto.Nome,
				Cidade = aeroportoDto.Cidade,
				Uf = aeroportoDto.Uf,
				Sigla = aeroportoDto.Sigla,
				Latitude = aeroportoDto.Latitude,
				Longitude = aeroportoDto.Longitude
			};

			try
			{
				db.Aeroportos.Add(aeroporto);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarAeroporto), new { id = aeroporto.IdAeroporto }, aeroporto);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar aeroporto: {ex.Message}");
			}
		}

		[HttpPost("CadastrarPassagem")]
		public IActionResult CadastrarPassagem([FromQuery] PassagemDto passagemDto)
		{
			if (string.IsNullOrEmpty(passagemDto.Moeda))
				return BadRequest("A moeda é obrigatória.");

			var tarifaExistente = db.Tarifas.Any(t => t.IdTarifa == passagemDto.TarifaIdTarifa);
			if (!tarifaExistente)
				return BadRequest("Não existe uma tarifa com esse ID.");

			var vooExistente = db.Voos.Any(v => v.IdVoo == passagemDto.IdVoo1);
			if (!vooExistente)
				return BadRequest("Não existe um voo com esse ID.");

			vooExistente = db.Voos.Any(v => v.IdVoo == passagemDto.IdVoo2);
			if (!vooExistente)
				return BadRequest("Não existe um voo com esse ID.");

			var codExistente = db.Companhiaaereas.Any(c => c.Cod == passagemDto.CompanhiaAereaCod);
			if (!codExistente)
				return BadRequest("Não existe uma companhia aérea com esse código.");

			if (!Enum.IsDefined(typeof(TipoPassagem), passagemDto.TipoPassagem))
				return BadRequest("O tipo de passagem é inválido.");
			
			double valorPassagem = passagemDto.TipoPassagem switch
			{
				TipoPassagem.Basica => passagemService.calcularValorBasica(),
				TipoPassagem.Business => passagemService.calcularValorBusiness(),
				TipoPassagem.Premium => passagemService.calcularValorPremium(),
				_ => throw new InvalidOperationException("Tipo de passagem inválido.")
			};

			var passagem = new Passagem
			{
				Moeda = passagemDto.Moeda,
				TarifaIdTarifa = passagemDto.TarifaIdTarifa,
				ValorbagagemId = passagemDto.ValorBagagemId,
				IdVoo1 = passagemDto.IdVoo1,
				IdVoo2 = passagemDto.IdVoo2,
				CompanhiaAereaCod = passagemDto.CompanhiaAereaCod,
				ValorPassagem = valorPassagem,
			};

			try
			{
				db.Passagems.Add(passagem);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarPassagem), new { id = passagem.IdPassagem }, passagem);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar passagem: {ex.Message}");
			}
		}
	}
}


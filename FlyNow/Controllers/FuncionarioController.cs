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
			var voo1 = db.Voos.FirstOrDefault(v => v.IdVoo == passagemDto.IdVoo1);
			if (voo1 == null)
				return BadRequest("Não existe um voo com o ID fornecido para Voo1.");

			var voo2 = passagemDto.IdVoo2 != 0
																									? db.Voos.FirstOrDefault(v => v.IdVoo == passagemDto.IdVoo2)
																									: null;

			var companhia = db.Companhiaaereas.FirstOrDefault(c => c.Cod == passagemDto.CompanhiaAereaCod);
			if (companhia == null)
				return BadRequest("Não existe uma companhia aérea com esse código.");

			if (!Enum.IsDefined(typeof(TipoPassagem), passagemDto.TipoPassagem))
				return BadRequest("O tipo de passagem é inválido.");

			double valorPassagem = 0;

			if (voo1 != null)
			{
				valorPassagem += passagemService.CalcularValor(passagemDto.TipoPassagem, voo1.EhInternacional == 1);
			}

			if (voo2 != null)
			{
				valorPassagem += passagemService.CalcularValor(passagemDto.TipoPassagem, voo2.EhInternacional == 1);
			}

			var valorBagagemInfo = db.Valorbagagems.FirstOrDefault(vb => vb.Id == passagemDto.ValorBagagemId);
			if (valorBagagemInfo == null)
				return BadRequest("Informações de bagagem não encontradas.");

			double valorTotalBagagem = passagemService.CalcularValorBagagem(
											valorBagagemInfo.ValorPrimeiraBagagem ?? 0,
											valorBagagemInfo.ValorBagagemAdicional ?? 0
			);

			double valorTotalTarifa = valorPassagem + valorTotalBagagem;

			int tarifaId = CalcularEAdicionarTarifa(valorTotalTarifa, passagemDto.CompanhiaAereaCod);

			var isInternacional = voo1.EhInternacional == 1 || (voo2?.EhInternacional == 1);
			var moeda = isInternacional ? "USD" : "BRL";

			var passagem = new Passagem
			{
				Moeda = moeda,
				TarifaIdTarifa = tarifaId,
				ValorbagagemId = passagemDto.ValorBagagemId,
				IdVoo1 = passagemDto.IdVoo1,
				IdVoo2 = passagemDto.IdVoo2 != 0 ? passagemDto.IdVoo2 : (int?)null,
				CompanhiaAereaId = passagemDto.CompanhiaAereaCod,
				ValorPassagem = valorPassagem,
				Status = passagemDto.Status,
			};

			try
			{
				db.Passagems.Add(passagem);
				db.SaveChanges();
				return CreatedAtAction(nameof(CadastrarPassagem), new { id = passagem.IdPassagem }, passagem);
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cadastrar passagem: {ex.Message} - Inner Exception: {ex.InnerException?.Message}");
			}
		}

		private int CalcularEAdicionarTarifa(double valorTotal, int companhiaAereaCod)
		{
			var companhia = db.Companhiaaereas.FirstOrDefault(c => c.Cod == companhiaAereaCod);
			if (companhia == null)
				throw new InvalidOperationException("Companhia aérea não encontrada.");

			double? taxaRemuneracao = companhia.TaxaRemuneracao;
			double? valorComRemuneracao = valorTotal + (valorTotal * taxaRemuneracao / 100);

			var tarifa = new Tarifa
			{
				Valor = valorComRemuneracao,
				CompanhiaaereaCod = companhiaAereaCod
			};

			db.Tarifas.Add(tarifa);
			db.SaveChanges();

			return tarifa.IdTarifa;
		}

		[HttpPost("RemarcarPassagem")]
		public IActionResult RemarcarPassagem(int passagemId, int novoVoo1Id, int? novoVoo2Id, int novoAssentoId)
		{
			var passagem = db.Passagems.FirstOrDefault(p => p.IdPassagem == passagemId);
			if (passagem == null || passagem.Status != StatusPassagemDto.Cancelada)
				return BadRequest("Passagem não encontrada ou não está cancelada.");

			var novoVoo1 = db.Voos.FirstOrDefault(v => v.IdVoo == novoVoo1Id);
			if (novoVoo1 == null)
				return BadRequest("Novo voo 1 não encontrado.");

			var novoVoo2 = novoVoo2Id.HasValue ? db.Voos.FirstOrDefault(v => v.IdVoo == novoVoo2Id.Value) : null;
			if (novoVoo2Id.HasValue && novoVoo2 == null)
				return BadRequest("Novo voo 2 não encontrado.");

			var assento = db.Assentos.FirstOrDefault(a => a.IdAssento == novoAssentoId);
			if (assento == null || assento.Ocupado == 1)
				return BadRequest("Assento não disponível.");

			passagem.Status = StatusPassagemDto.Ativa;
			passagem.IdVoo1 = novoVoo1Id;
			passagem.IdVoo2 = novoVoo2Id;

			var bilhetes = db.Bilhetes.Where(b => b.PassagemIdPassagem == passagemId).ToList();
			foreach (var bilhete in bilhetes)
			{
				bilhete.IdAssento = novoAssentoId;
			}

			assento.Ocupado = 1;

			try
			{
				db.SaveChanges();
				return Ok("Passagem remarcada com sucesso.");
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao remarcar passagem: {ex.Message}");
			}
		}
	}
}


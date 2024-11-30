using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using FlyNow.Interfaces;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PassageiroVipController : ControllerBase
	{
		private readonly FlyNowContext _context;
		private readonly FlyNow.Interfaces.ILog _logServico;

		public PassageiroVipController(FlyNowContext context, ILog logServico)
		{
			_context = context;
			_logServico = logServico;
		}

		// Método para alterar o status do bilhete sem custo adicional para VIPs
		[HttpPost("AlterarStatusBilhete")]
		public IActionResult AlterarStatusBilhete(int idPassageiro, int idPassagem, StatusPassagem novoStatus)
		{
			// Verifica se o passageiro é VIP
			bool isVip = _context.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);

			if (!isVip)
			{
				return BadRequest("Apenas passageiros VIP podem alterar o status do bilhete sem custo.");
			}

			// Busca o bilhete e a passagem associada para atualizar o status
			var bilhete = _context.Bilhetes.FirstOrDefault(b => b.PassagemIdPassagem == idPassagem && b.PassageiroIdPassageiro == idPassageiro);
			if (bilhete == null)
			{
				return NotFound("Bilhete não encontrado para o passageiro especificado.");
			}

			// Atualiza o status do bilhete
			bilhete.StatusPassageiro = novoStatus;

			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao alterar o status do bilhete: {ex.Message}");
			}

			_logServico.RegistrarLog($"Passagem {idPassagem} do passageiro {idPassageiro} cancelada sem custo.");
			return Ok("Status do bilhete alterado com sucesso sem custo adicional para passageiro VIP.");
		}

		// Método para calcular o custo de bagagem com desconto para VIPs
		[HttpGet("CalcularCustoBagagem")]
		public IActionResult CalcularCustoBagagem(int idPassageiro, int quantidadeBagagens)
		{
			// Verifica se o passageiro é VIP
			bool isVip = _context.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);
			decimal custoPorBagagem = 100; // Exemplo de custo padrão por bagagem
			decimal custoTotal = 0;

			if (isVip)
			{
				// Primeira bagagem gratuita para passageiros VIP
				quantidadeBagagens -= 1;
				custoTotal = 0;

				// Se houver bagagens adicionais, aplica o desconto de 50%
				if (quantidadeBagagens > 0)
				{
					custoTotal += quantidadeBagagens * (custoPorBagagem * 0.5m);
				}
			}
			else
			{
				// Para não VIPs, cobra o custo total
				custoTotal = quantidadeBagagens * custoPorBagagem;
			}

			return Ok(new { CustoTotal = custoTotal });
			_logServico.RegistrarLog($"Cálculo de custo de bagagem realizado para passageiro {idPassageiro}, quantidade de bagagens: {quantidadeBagagens}.");

		}

		// Método para cancelar a passagem sem custo adicional para VIPs
		[HttpPost("CancelarPassagemSemCusto")]
		public IActionResult CancelarPassagemSemCusto(int idPassageiro, int idPassagem)
		{
			// Verifica se o passageiro é VIP
			bool isVip = _context.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);

			if (!isVip)
			{
				return BadRequest("Apenas passageiros VIP podem cancelar passagens sem custo.");
			}

			// Busca a passagem e o bilhete associados e remove a passagem
			var passagem = _context.Passagems.FirstOrDefault(p => p.IdPassagem == idPassagem);
			var bilhete = _context.Bilhetes.FirstOrDefault(b => b.PassagemIdPassagem == idPassagem && b.PassageiroIdPassageiro == idPassageiro);

			if (passagem == null || bilhete == null)
			{
				return NotFound("Passagem ou bilhete não encontrados para o passageiro especificado.");
			}

			_context.Bilhetes.Remove(bilhete);
			_context.Passagems.Remove(passagem);

			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cancelar a passagem: {ex.Message}");
			}

			_logServico.RegistrarLog($"Passagem {idPassagem} do passageiro {idPassageiro} cancelada sem custo.");
			return Ok("Passagem cancelada com sucesso sem custo adicional para passageiro VIP.");
		}
	}
}
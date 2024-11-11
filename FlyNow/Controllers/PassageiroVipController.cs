using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PassageiroVipController : ControllerBase
	{
		private readonly FlyNowContext _context;

		public PassageiroVipController(FlyNowContext context)
		{
			_context = context;
		}

		// Método para alterar voo sem custo adicional para VIPs
		[HttpPost("AlterarVooSemCusto")]
		public IActionResult AlterarVooSemCusto(int idPassageiro, int idBilhete, int novoVooId)
		{
			// Verifica se o passageiro é VIP
			bool isVip = _context.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);

			if (!isVip)
			{
				return BadRequest("Apenas passageiros VIP podem alterar voos sem custo.");
			}

			// Busca o bilhete e atualiza o voo
			var bilhete = _context.Bilhetes.FirstOrDefault(b => b.IdBilhete == idBilhete && b.PassageiroIdPassageiro == idPassageiro);
			if (bilhete == null)
			{
				return NotFound("Bilhete não encontrado para o passageiro especificado.");
			}

			bilhete.VooId = novoVooId;

			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao alterar o voo: {ex.Message}");
			}

			return Ok("Voo alterado com sucesso sem custo adicional para passageiro VIP.");
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
		}

		// Método para cancelar voo sem custo adicional para VIPs
		[HttpPost("CancelarVooSemCusto")]
		public IActionResult CancelarVooSemCusto(int idPassageiro, int idBilhete)
		{
			// Verifica se o passageiro é VIP
			bool isVip = _context.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);

			if (!isVip)
			{
				return BadRequest("Apenas passageiros VIP podem cancelar voos sem custo.");
			}

			// Busca o bilhete e remove-o
			var bilhete = _context.Bilhetes.FirstOrDefault(b => b.IdBilhete == idBilhete && b.PassageiroIdPassageiro == idPassageiro);
			if (bilhete == null)
			{
				return NotFound("Bilhete não encontrado para o passageiro especificado.");
			}

			_context.Bilhetes.Remove(bilhete);

			try
			{
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cancelar o voo: {ex.Message}");
			}

			return Ok("Voo cancelado com sucesso sem custo adicional para passageiro VIP.");
		}
	}
}

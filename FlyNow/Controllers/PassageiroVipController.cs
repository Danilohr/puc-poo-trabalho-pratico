using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using FlyNow.Interfaces;
using FlyNow.Services;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PassageiroVipController : Base
	{
		public PassageiroVipController() : base(new FlyNowContext(), new ServicoLog()) { }
		public PassageiroVipController(FlyNowContext db) : base(db, new ServicoLog()) { }

		// Método para alterar o status do bilhete sem custo adicional para VIPs
		[HttpPost("AlterarStatusBilhete")]
		public IActionResult AlterarStatusBilhete(int idPassageiro, int idPassagem, string novoStatusPassagem)
		{
			// Verifica se o passageiro é VIP
			bool isVip = db.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);

			if (!isVip)
			{
				return BadRequest("Apenas passageiros VIP podem alterar o status do bilhete sem custo.");
			}

			// Busca o bilhete e a passagem associada para atualizar o status
			var bilhete = db.Bilhetes.FirstOrDefault(b => b.PassagemIdPassagem == idPassagem && b.PassageiroIdPassageiro == idPassageiro);
			if (bilhete == null)
			{
				return NotFound("Bilhete não encontrado para o passageiro especificado.");
			}

			// Atualiza o status do bilhete
			bilhete.StatusPassageiro = novoStatusPassagem;

			try
			{
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao alterar o status do bilhete: {ex.Message}");
			}

			logServico.RegistrarLog($"Passagem {idPassagem} do passageiro {idPassageiro} cancelada sem custo.");
			return Ok("Status do bilhete alterado com sucesso sem custo adicional para passageiro VIP.");
		}

		// Método para calcular o custo de bagagem com desconto para VIPs
		[HttpGet("CalcularCustoBagagem")]
		public IActionResult CalcularCustoBagagem(int idPassageiro, int quantidadeBagagens)
		{
			// Verifica se o passageiro é VIP
			bool isVip = db.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);
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

			logServico.RegistrarLog($"Cálculo de custo de bagagem realizado para passageiro {idPassageiro}, quantidade de bagagens: {quantidadeBagagens}.");
			return Ok(new { CustoTotal = custoTotal });

		}

		// Método para cancelar a passagem sem custo adicional para VIPs
		[HttpPost("CancelarPassagemSemCusto")]
		public IActionResult CancelarPassagemSemCusto(int idPassageiro, int idPassagem)
		{
			// Verifica se o passageiro é VIP
			bool isVip = db.PassageiroVIPs.Any(pv => pv.PassageiroIdPassageiro == idPassageiro);

			if (!isVip)
			{
				return BadRequest("Apenas passageiros VIP podem cancelar passagens sem custo.");
			}

			// Busca a passagem e o bilhete associados e remove a passagem
			var passagem = db.Passagems.FirstOrDefault(p => p.IdPassagem == idPassagem);
			var bilhete = db.Bilhetes.FirstOrDefault(b => b.PassagemIdPassagem == idPassagem && b.PassageiroIdPassageiro == idPassageiro);

			if (passagem == null || bilhete == null)
			{
				return NotFound("Passagem ou bilhete não encontrados para o passageiro especificado.");
			}

			db.Bilhetes.Remove(bilhete);
			db.Passagems.Remove(passagem);

			try
			{
				db.SaveChanges();
			}
			catch (Exception ex)
			{
				return BadRequest($"Erro ao cancelar a passagem: {ex.Message}");
			}

			logServico.RegistrarLog($"Passagem {idPassagem} do passageiro {idPassageiro} cancelada sem custo.");
			return Ok("Passagem cancelada com sucesso sem custo adicional para passageiro VIP.");
		}
	}
}
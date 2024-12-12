using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlyNow.Interfaces;
using FlyNow.Controllers;
using FlyNow.Services;

[ApiController]
[Route("api/[controller]")]
public class PassagemController : Base
{
	public PassagemController() : base (new FlyNowContext(), new ServicoLog()) {}
	public PassagemController(FlyNowContext db) : base (db, new ServicoLog()) { }

	// GET: api/Passagens
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Passagem>>> GetPassagens()
	{
		logServico.RegistrarLog("Consulta realizada para listar todas as passagens.");
		return await db.Passagems.ToListAsync();
	}

	[HttpGet]
	[Route("verificar-status/{idPassageiro}")]
	public IActionResult VerificarStatusPassagem(int idPassageiro)
	{
		try
		{
			// Busca o passageiro pelo ID
			var passageiro = db.Passageiros
					.FirstOrDefault(p => p.IdPassageiro == idPassageiro);
			if (passageiro == null)
				return NotFound($"Passageiro com ID {idPassageiro} não encontrado.");

			// Busca os bilhetes associados ao passageiro
			var bilhetes = db.Bilhetes
					.Include(b => b.PassagemIdPassagemNavigation)
							.ThenInclude(p => p.Voo1) // Inclui informações do voo principal
					.Include(b => b.PassagemIdPassagemNavigation)
							.ThenInclude(p => p.Voo2) // Inclui informações do voo secundário
					.Where(b => b.PassageiroIdPassageiro == idPassageiro)
					.ToList();

			if (!bilhetes.Any())
				return NotFound("Nenhum bilhete encontrado para o passageiro.");

			// Processa o status de cada bilhete
			var statusBilhetes = bilhetes.Select(bilhete => new
			{
				StatusPassagem = bilhete.StatusPassageiro.ToString(),
				Voos = new List<object>
						{
								bilhete.PassagemIdPassagemNavigation.Voo1 != null ? new {
										Voo = bilhete.PassagemIdPassagemNavigation.Voo1.CodVoo,
										bilhete.PassagemIdPassagemNavigation.Voo1.Data,
										bilhete.PassagemIdPassagemNavigation.Voo1.Duracao
								} : null,
								bilhete.PassagemIdPassagemNavigation.Voo2 != null ? new {
										Voo = bilhete.PassagemIdPassagemNavigation.Voo2.CodVoo,
										bilhete.PassagemIdPassagemNavigation.Voo2.Data,
										bilhete.PassagemIdPassagemNavigation.Voo2.Duracao
								} : null
						}.Where(v => v != null).ToList()
			});

			// Retorna o resultado
			return Ok(new
			{
				Passageiro = passageiro.Nome,
				Bilhetes = statusBilhetes
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Erro ao verificar status da passagem: {ex.Message}");
		}
	}






}

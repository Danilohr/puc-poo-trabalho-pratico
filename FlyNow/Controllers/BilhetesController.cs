using FlyNow.Controllers;
using FlyNow.Data;
using FlyNow.EfModels;
using FlyNow.Interfaces;
using FlyNow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BilhetesController : Base
{
	public BilhetesController() : base(new FlyNowContext(), new ServicoLog()) {	}
	public BilhetesController(FlyNowContext context) : base(context, new ServicoLog()) { }

	// GET: api/Bilhetes
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Bilhete>>> GetBilhetes()
	{
		return await db.Bilhetes
				.Include(b => b.Passageiro) // Inclui dados do Passageiro relacionado
				.Include(b => b.Passagem)   // Inclui dados da Passagem relacionada
				.ToListAsync();
	}

	// Método para realizar o check-in
	private List<Voo> _Voo = new List<Voo>();

	[HttpPost("{id}/checkin")]
	public IActionResult RealizarCheckIn(int idBilhete)
	{
		var bilhete = base.db.Bilhetes.FirstOrDefault(p => p.PassagemIdPassagem == idBilhete);
		var Voo = base.db.Voos.FirstOrDefault(v => v.IdVoo == db.Passagems.FirstOrDefault(p => p.IdPassagem == bilhete.PassagemIdPassagem).Voo1.IdVoo);
		if (Voo == null) return NotFound("Passagem não encontrada.");

		var agora = DateTime.Now;

		var periodoInicio = Voo.Data.AddHours(-48);
		var periodoFim = Voo.Data.AddMinutes(-30);

		if (agora < periodoInicio || agora > periodoFim)
		{
			return BadRequest("Check-in fora do período permitido.");
		}

		bilhete.StatusPassageiro = StatusPassagem.EmbarqueRealizado;
		base.db.SaveChanges();

		logServico.RegistrarLog($"Check-in realizado para o bilhete com ID {idBilhete}.");

		return Ok("Check-in realizado com sucesso.");
	}

	// Método para registrar status de NO SHOW se o passageiro não embarcou
	[HttpPost("{id}/registrarNoShow")]
	public IActionResult RegistrarNoShow(int id)
	{
		var bilhete = base.db.Bilhetes.FirstOrDefault(p => p.PassageiroIdPassageiro == id);

		if (bilhete == null)
		{
			return NotFound("Passagem não encontrada.");
		}

		if (bilhete.StatusPassageiro != StatusPassagem.CheckInRealizado && bilhete.StatusPassageiro != StatusPassagem.EmbarqueRealizado)
		{
			bilhete.StatusPassageiro = StatusPassagem.NoShow;

			logServico.RegistrarLog($"No Show registrado para o bilhete com ID {id}.");

			return Ok("Status NO SHOW registrado.");
		}

		return BadRequest("Passageiro já realizou o embarque.");
	}
}
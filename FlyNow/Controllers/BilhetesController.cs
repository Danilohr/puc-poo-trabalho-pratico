using FlyNow.Controllers;
using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class BilhetesController : ControllerBase
{
	private readonly FlyNowContext _context;

	public BilhetesController(FlyNowContext context)
	{
		_context = context;
	}

	// GET: api/Bilhetes
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Bilhete>>> GetBilhetes()
	{
		return await _context.Bilhetes
				.Include(b => b.PassageiroIdPassageiroNavigation) // Inclui dados do Passageiro relacionado
				.Include(b => b.PassagemIdPassagemNavigation)   // Inclui dados da Passagem relacionada
				.ToListAsync();
	}

	// Método para realizar o check-in
	private static List<Voo> _Voo = new List<Voo>();
	private static List<Bilhete> _bilhete = new List<Bilhete>();
	private static StatusPassagem StatusPassagem;

	[HttpPost("{id}/checkin")]
	public IActionResult RealizarCheckIn(int id)
	{
		var Voo = _Voo.FirstOrDefault(p => p.IdVoo == id);
		var bilhete = _bilhete.FirstOrDefault(p => p.PassagemIdPassagem == id);
		if (Voo == null) return NotFound("Passagem não encontrada.");

		var agora = DateTime.Now;
		var periodoInicio = Voo.Data.AddHours(-48);
		var periodoFim = Voo.Data.AddMinutes(-30);

		if (agora < periodoInicio || agora > periodoFim)
		{
			return BadRequest("Check-in fora do período permitido.");
		}

		bilhete.StatusPassageiro = StatusPassagem.EmbarqueRealizado;

		return Ok("Check-in realizado com sucesso.");
	}

	// Método para registrar status de NO SHOW se o passageiro não embarcou
	[HttpPost("{id}/registrarNoShow")]
	public IActionResult RegistrarNoShow(int id)
	{
		var bilhete = _bilhete.FirstOrDefault(p => p.PassageiroIdPassageiro == id);

		if (bilhete == null)
		{
			return NotFound("Passagem não encontrada.");
		}

		if (bilhete.StatusPassageiro != StatusPassagem.CheckInRealizado && bilhete.StatusPassageiro != StatusPassagem.EmbarqueRealizado)
		{
			bilhete.StatusPassageiro = StatusPassagem.NoShow;
			return Ok("Status NO SHOW registrado.");
		}

			return BadRequest("Passageiro já realizou o embarque.");
	}
}

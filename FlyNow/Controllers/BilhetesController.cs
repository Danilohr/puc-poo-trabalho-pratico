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
				.Include(b => b.Passageiro) // Inclui dados do Passageiro relacionado
				.Include(b => b.Passagem)   // Inclui dados da Passagem relacionada
				.ToListAsync();
	}
}

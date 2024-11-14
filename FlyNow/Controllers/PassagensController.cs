using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class PassagensController : ControllerBase
{
	private readonly FlyNowContext _context;

	public PassagensController(FlyNowContext context)
	{
		_context = context;
	}

	// GET: api/Passagens
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Passagem>>> GetPassagens()
	{
		return await _context.Passagems.ToListAsync();
	}
}

using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FlyNow.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class PassagensController : ControllerBase
{
	private readonly FlyNowContext _context;
	private readonly FlyNow.Interfaces.ILog _logServico;

	public PassagensController(FlyNowContext context, ILog logServico)
	{
		_context = context;
		_logServico = logServico;
	}

	// GET: api/Passagens
	[HttpGet]
	public async Task<ActionResult<IEnumerable<Passagem>>> GetPassagens()
	{
		_logServico.RegistrarLog("Consulta realizada para listar todas as passagens.");
		return await _context.Passagems.ToListAsync();
	}
}

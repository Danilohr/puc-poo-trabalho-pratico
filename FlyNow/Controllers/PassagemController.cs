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
}

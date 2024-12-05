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

	private static List<Voo> _Voo = new List<Voo>();
	private static List<Bilhete> _bilhete = new List<Bilhete>();
	private static StatusPassagem StatusPassagem;

	// Método para realizar o check-in
	[HttpPost("{id}/realizarCheckIn")]
	public IActionResult RealizarCheckIn(int id)
	{
		var passagem = _context.Passagems
				.Include(p => p.Bilhetes)
				.FirstOrDefault(p => p.IdPassagem == id);

		if (passagem == null)
			return NotFound("Passagem não encontrada.");

		var voo = _context.Voos.FirstOrDefault(v => v.IdVoo == passagem.IdVoo1);

		if (voo == null)
			return NotFound("Voo não encontrado.");

		var horaAtual = DateTime.Now;

		// Verifica se está dentro do período permitido
		if (horaAtual >= voo.Data.AddHours(48) && horaAtual <= voo.Data.AddMinutes(30))
		{
			foreach (var bilhete in passagem.Bilhetes)
			{
				bilhete.StatusPassageiro = StatusPassagem.CheckInRealizado;
				_context.SaveChanges();
			}

			// Gerar cartão de embarque
			var cartaoEmbarque = new
			{
				NomePassageiro = passagem.Bilhetes.First().PassageiroIdPassageiroNavigation.Nome,
				CodigoVoo = voo.CodVoo,
				HorarioEmbarque = voo.Data.AddMinutes(-30),
				Assento = "A definir" // Pode ser integrado com a lógica de assentos, não olhei como fazer isso.
			};

			return Ok(new { Mensagem = "Check-in realizado com sucesso.", CartaoEmbarque = cartaoEmbarque });
		}

		foreach (var bilhete in passagem.Bilhetes)
		{
			// Verifica se o status do passageiro ainda não foi atualizado para "CheckInRealizado"
			if (bilhete.StatusPassageiro != StatusPassagem.CheckInRealizado)
			{
				bilhete.StatusPassageiro = StatusPassagem.NoShow;
			}
		}

		_context.SaveChanges();

		return BadRequest("Fora do período de check-in. Status de No Show registrado.");
	}

	[HttpPost("{id}/registrarNoShow")]
	public IActionResult RegistrarNoShow(int id)
	{
		var bilhete = _context.Bilhetes.FirstOrDefault(b => b.PassagemIdPassagem == id);

		if (bilhete == null)
			return NotFound("Bilhete não encontrado.");

		if (bilhete.StatusPassageiro == StatusPassagem.CheckInRealizado || bilhete.StatusPassageiro == StatusPassagem.EmbarqueRealizado)
			return BadRequest("Passageiro já realizou check-in ou embarque.");

		bilhete.StatusPassageiro = StatusPassagem.NoShow;
		_context.SaveChanges();

		return Ok("Status NO SHOW registrado.");
	}

	[HttpPost("{id}/registrarEmbarque")]
	public IActionResult RegistrarEmbarque(int id)
	{
		var bilhete = _context.Bilhetes
				.Include(b => b.PassagemIdPassagemNavigation)
				.FirstOrDefault(b => b.PassagemIdPassagem == id);

		if (bilhete == null)
			return NotFound("Bilhete não encontrado.");

		if (bilhete.StatusPassageiro != StatusPassagem.CheckInRealizado)
			return BadRequest("O passageiro não realizou o check-in.");

		bilhete.StatusPassageiro = StatusPassagem.EmbarqueRealizado;
		_context.SaveChanges();

		return Ok("Embarque realizado com sucesso.");
	}

	[HttpGet]
	[Route("api/bilhete/verificar-status/{idPassagem}")]
	public IActionResult VerificarStatusPassagem(int idPassagem)
	{
		try
		{
			// Buscar o bilhete no banco de dados com base no id da passagem
			var bilhete = _context.Bilhetes
					.Where(b => b.PassagemIdPassagem == idPassagem)
					.Select(b => new
					{
						b.StatusPassageiro,  // O status da passagem
						b.PassagemIdPassagem,
						b.PassageiroIdPassageiro
					})
					.FirstOrDefault();

			if (bilhete == null)
				return NotFound("Nenhum bilhete encontrado para a passagem informada.");

			return Ok(new
			{
				Mensagem = "Status encontrado com sucesso.",
				Bilhete = bilhete
			});
		}
		catch (Exception ex)
		{
			return StatusCode(500, $"Erro ao verificar o status do bilhete: {ex.Message}");
		}
	}

}

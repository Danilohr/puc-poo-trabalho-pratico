using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VooController : Base
	{
		public VooController(FlyNowContext context) : base(context)
		{
		}

		[HttpGet]
		public IActionResult Get()
		{
			List<Voo> lista = db.Voos.ToList();


			return Ok(lista);
		}

		[HttpPost]
		public IActionResult Post(Voo voo)
		{
			// Validação 
			List<Voo> lista = db.Voos.ToList();

			// Service
			//VooService.Post(voo);


			return Ok(lista);
		}
	}
}

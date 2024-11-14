using FlyNow.Data;
using FlyNow.EfModels;
using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PassagemController : Base
	{
		private static List<Passagem> _passagens = new List<Passagem>();
		public PassagemController(FlyNowContext context) : base(context)
		{

		}

	}
}

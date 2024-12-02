using FlyNow.Data;
using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CompanhiaAereaController : Base
	{
		public CompanhiaAereaController(FlyNowContext context) : base(context)
		{
		}


	}
}

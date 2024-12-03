using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	public class Base : Controller
	{
		internal readonly Data.FlyNowContext db;

		public Base(Data.FlyNowContext context)
		{
			db = context;
		}
	}
}

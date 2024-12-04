using FlyNow.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	public class Base : Controller
	{
		internal readonly Data.FlyNowContext db;
		protected readonly FlyNow.Interfaces.ILog logServico;

		public Base(Data.FlyNowContext context, ILog log)
		{
			db = context;
			logServico = log;
		}
		public Base(ILog log)
		{
			db = new Data.FlyNowContext();
			logServico = log;
		}
	}
}

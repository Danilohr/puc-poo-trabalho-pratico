using FlyNow.Data;
using FlyNow.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlyNow.Controllers
{
	public class Base : Controller
	{
		internal readonly Data.FlyNowContext db;
		protected readonly FlyNow.Interfaces.ILog logServico;
		private FlyNowContext context;

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

		public Base(FlyNowContext context)
		{
			this.context = context;
		}
	}
}

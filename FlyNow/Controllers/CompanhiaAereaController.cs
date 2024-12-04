using FlyNow.Data;
using FlyNow.Services;

namespace FlyNow.Controllers
{
	public class CompanhiaAereaController : Base
	{
		public CompanhiaAereaController() : base(new FlyNowContext(), new ServicoLog()) { }
		public CompanhiaAereaController(FlyNowContext context) : base(context, new ServicoLog()) { }
	}
}

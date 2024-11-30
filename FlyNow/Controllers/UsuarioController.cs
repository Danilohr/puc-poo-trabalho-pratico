using FlyNow.Data;
using FlyNow.EfModels;
using FlyNow.Interfaces;
using FlyNow.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Security.Cryptography.Xml;

namespace FlyNow.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsuarioController : Base
	{
		public UsuarioController() : base(new FlyNowContext(), new ServicoLog()) { }
		public UsuarioController(FlyNowContext db) : base(db, new ServicoLog()) { }


	}
}

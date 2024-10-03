using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{

	public class Usuario : _Base
	{
		private string Login;
		private string Senha;

		public Usuario(string login, string senha)
		{
			Login = login;
			Senha = senha;
		}
	}
}

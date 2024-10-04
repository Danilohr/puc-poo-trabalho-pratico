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

		public bool logar()
		{
			string login;
			string senha;

			Console.WriteLine("Informe o seu login");
			login = Console.ReadLine();
			Console.WriteLine("Informe a sua senha");
			senha = Console.ReadLine();

			if (senha != this.Senha || login != this.Login)
				return false;
			else
				return true;
		}
	}
}

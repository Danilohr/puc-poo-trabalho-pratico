using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Funcionario : _Base
	{
		private string Nome;
		private string Cpf;
		private string Email;

		public Funcionario(string nome, string cpf, string email)
		{
			Nome = nome;
			Cpf = cpf;
			Email = email;
		}


		
	}
}

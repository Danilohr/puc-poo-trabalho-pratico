using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Funcionario : _Base
	{
		private string nome;
		private string cpf;
		private string email;

		public Funcionario(string nome, string cpf, string email)
		{
			this.nome = nome;
			this.cpf = cpf;
			this.email = email;
		}
	}
}

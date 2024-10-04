using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Viajante : _Base
	{
		private string Nome;
		private string Cpf;
		private string Rg;

		public Viajante(string nome, string cpf, string rg)
		{
			Nome = nome;
			Cpf = cpf;
			Rg = rg;
		}
	}
}

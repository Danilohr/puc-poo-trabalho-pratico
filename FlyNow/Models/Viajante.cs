using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Viajante : _Base
	{
		private string nome;
		private string cpf;
		private string rg;

		public Viajante(string _nome, string _cpf, string _rg)
		{
			this.nome = _nome;
			this.cpf = _cpf;
			this.rg = _rg;
		}
	}
}

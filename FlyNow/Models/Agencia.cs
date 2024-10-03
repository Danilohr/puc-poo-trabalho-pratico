using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Agencia : _Base
	{
		private string nome;
		private double taxaAgencia;

		public Agencia(string nome, double taxaAgencia)
		{
			this.nome = nome;
			this.taxaAgencia = taxaAgencia;
		}
	}
}

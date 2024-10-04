using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Agencia : _Base
	{
		private string Nome;
		private double TaxaAgencia;

		public Agencia(string nome, double taxaAgencia)
		{
			Nome = nome;
			TaxaAgencia = taxaAgencia;
		}
	}
}

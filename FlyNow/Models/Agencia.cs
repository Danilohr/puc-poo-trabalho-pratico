using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Agencia : _Base
	{
		private string Nome;
		public double TaxaAgencia { get; }
		private List<CompanhiaAerea> ListComanhiaAerea { get; } = new List<CompanhiaAerea>();

		public Agencia(string nome, double taxaAgencia)
		{
			Nome = nome;
			TaxaAgencia = taxaAgencia;
		}

		public void adicionarComanhiaAerea(CompanhiaAerea a)
		{
			ListComanhiaAerea.Add(a);
		}
	}
}

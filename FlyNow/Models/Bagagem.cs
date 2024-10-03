using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Bagagem : _Base
	{
		private double ValorPrimeiraBagagem;
		private double ValorBagagemAdicional;

		public Bagagem(double valorPrimeiraBagagem, double valorBagagemAdicional)
		{
			ValorPrimeiraBagagem = valorPrimeiraBagagem;
			ValorBagagemAdicional = valorBagagemAdicional;
		}
	}
}

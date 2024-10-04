using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class ValorBagagem : _Base
	{
		private double ValorPrimeiraBagagem;
		private double ValorBagagemAdicional;

		public ValorBagagem(double valorPrimeiraBagagem, double valorBagagemAdicional)
		{
			ValorPrimeiraBagagem = valorPrimeiraBagagem;
			ValorBagagemAdicional = valorBagagemAdicional;
		}
	}
}

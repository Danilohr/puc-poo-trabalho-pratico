using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public abstract class Passagem : _Base
	{
		internal Voo[] Voos;
		internal ValorBagagem ValorBagagem;
		internal CompanhiaAerea CompanhiaOperadora;
		internal string Moeda;
		internal Tarifa Tarifa;

		protected Passagem(Voo[] voos, ValorBagagem valorBagagem, string moeda)
		{
			Voos = voos;
			ValorBagagem = valorBagagem;
			Moeda = moeda;
			CompanhiaOperadora = voos[0].CompanhiaOperadora;
		}

		public abstract Tarifa getTarifa();
	}
}

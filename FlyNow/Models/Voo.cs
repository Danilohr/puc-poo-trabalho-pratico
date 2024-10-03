using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Voo : _Base
	{
		private Aeroporto Origem;
		private Aeroporto Destino;

		public Voo(Aeroporto origem, Aeroporto destino)
		{
			Origem = origem;
			Destino = destino;
		}
	}
}

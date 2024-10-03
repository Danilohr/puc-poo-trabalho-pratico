using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Bilhete : _Base
	{
		private Passagem passagem;
		private Viajante passageiro;

		public Bilhete(Passagem _passagem, Viajante _passageiro)
		{
			this.passagem = _passagem;
			this.passageiro = _passageiro;
		}
	}
}

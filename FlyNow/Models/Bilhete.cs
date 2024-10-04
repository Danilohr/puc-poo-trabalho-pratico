using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Bilhete : _Base
	{
		private Passagem Passagem;
		private Viajante Passageiro;

		public Bilhete(Passagem passagem, Viajante passageiro)
		{
			Passagem = passagem;
			Passageiro = passageiro;
		}
	}
}

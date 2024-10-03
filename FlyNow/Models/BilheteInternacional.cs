using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class BilheteInternacional : Bilhete
	{
		private string passaporte;

		public BilheteInternacional(string _passaporte, Passagem _passagem, Viajante _passageiro) : base(_passagem, _passageiro)
		{
			this.passaporte = _passaporte;
		}
	}
}

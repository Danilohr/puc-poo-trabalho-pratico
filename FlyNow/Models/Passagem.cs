using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Passagem : _Base
	{
		private DateTime Data;
		private string CodVoo;
		private Voo Voo;
		private Bagagem ValorBagagem;
		private CompanhiaAerea CompanhiaOperadora;
		private decimal Moeda;

		public Passagem(DateTime data, string codVoo, Voo voo, Bagagem valorBagagem, CompanhiaAerea companhiaOperadora, decimal moeda)
		{
			Data = data;
			CodVoo = codVoo;
			Voo = voo;
			ValorBagagem = valorBagagem;
			CompanhiaOperadora = companhiaOperadora;
			Moeda = moeda;
		}
	}
}

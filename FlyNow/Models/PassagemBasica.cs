using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class PassagemBasica : Passagem
	{
		private double tarifa { get; set; }

		public PassagemBasica(double tarifa, DateTime data, string codVoo, Voo voo, Bagagem valorBagagem, CompanhiaAerea companhiaOperadora, decimal moeda) : base(data, codVoo, voo, valorBagagem, companhiaOperadora, moeda)
		{
		}
	}
}

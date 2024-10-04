using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Tarifa : _Base
	{
		private double Valor { get; }

		public Tarifa(double valor)
		{
			Valor = valor;
		}
	}
}

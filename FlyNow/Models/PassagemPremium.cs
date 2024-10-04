﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
  internal class PassagemPremium : Passagem
  {
		public PassagemPremium(Voo[] voos, ValorBagagem valorBagagem, string moeda) : base(voos, valorBagagem, moeda)
		{
		}

		public override Tarifa getTarifa()
		{
			return CompanhiaOperadora.GetTarifa(CompanhiaAerea.TipoVoo.Premium);
		}
	}
}

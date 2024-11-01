using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
  internal class PassagemBusiness : Passagem
  {
		public PassagemBusiness(Voo[] voos, ValorBagagem valorBagagem, string moeda) : base(voos, valorBagagem, moeda)
		{
		}

		public override Tarifa getTarifa(Agencia a)
		{
			return CompanhiaOperadora.GetTarifa(CompanhiaAerea.TipoVoo.Business, a);
		}
	}
}

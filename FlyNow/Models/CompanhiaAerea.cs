using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class CompanhiaAerea
	{
		private string Nome;
		private double Codigo;
		private string RazaoSocial;
		private string Cnpj;
		private double TaxaRemuneracao;

		public enum TipoVoo
		{
			Basica,
			Business,
			Premium
		}

		public CompanhiaAerea(double codigo, string nome, string razaoSocial, string cnpj)
		{
			Codigo = codigo;
			Nome = nome;
			RazaoSocial = razaoSocial;
			Cnpj = cnpj;
		}

		public ValorBagagem GetBagagem()
		{
			// vai puxar do banco e somar as duas
			return new ValorBagagem(100, 0);
		}

		internal Tarifa GetTarifa(TipoVoo tipoVoo)
		{
			switch (tipoVoo)
			{// vai puxar do banco
				case TipoVoo.Basica: return new Tarifa(1000);
				case TipoVoo.Business: return new Tarifa(3000);
				case TipoVoo.Premium: return new Tarifa(6000);
				default: return new Tarifa(0);
			}
		}

		public Passagem GerarPassagem(Voo[] voos, TipoVoo tipoVoo)
		{
			switch (tipoVoo)
			{
				case TipoVoo.Basica: 
					return new PassagemBasica(voos, this.GetBagagem(), "real");
				case TipoVoo.Business:
					return new PassagemBusiness(voos, this.GetBagagem(), "real");
				case TipoVoo.Premium:
					return new PassagemPremium(voos, this.GetBagagem(), "real");

				default: throw new Exception("Não foi possível gerar passagem!");
			}
		}
		public Passagem GerarPassagem(Voo[] voos, TipoVoo tipoVoo, string moeda)
		{
			switch (tipoVoo)
			{
				case TipoVoo.Basica:
					return new PassagemBasica(voos, this.GetBagagem(), moeda);
				case TipoVoo.Business:
					return new PassagemBusiness(voos, this.GetBagagem(), moeda);
				case TipoVoo.Premium:
					return new PassagemPremium(voos, this.GetBagagem(), moeda);

				default: throw new Exception("Não foi possível gerar passagem!");
			}
		}
	}
}

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

		internal Tarifa GetTarifa(TipoVoo tipoVoo, Agencia a)
		{
			double valorPassagem = 0;

			switch (tipoVoo)
			{// vai puxar do banco
				case TipoVoo.Basica: valorPassagem = 1000;
					break;
				case TipoVoo.Business: valorPassagem = 3000;
					break;
				case TipoVoo.Premium: valorPassagem = 6000;
					break;
				default: throw new Exception("Sem valor na tarifa");
			}

			valorPassagem += a.TaxaAgencia;

			Tarifa t = new Tarifa(valorPassagem);
			return t;
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
		public void cancelarVoo(Voo voo)
		{
			voo.Status = false;

			//para cancelar as passagens será feita uma busca no banco para as passagens que possuem esse voo
		}
	}
}

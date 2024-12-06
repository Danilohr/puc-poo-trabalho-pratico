using System;
using System.IO;
using FlyNow.DTOs;
using FlyNow.Interfaces;
namespace FlyNow.Services
{
	public class ServicoPassagem
	{
		public double calcularValor(TipoPassagem tipoPassagem)
		{
			return tipoPassagem switch
			{
				TipoPassagem.Basica => calcularValorBasica(),
				TipoPassagem.Business => calcularValorBusiness(),
				TipoPassagem.Premium => calcularValorPremium(),
				_ => throw new ArgumentException("Tipo de passagem inválido.")
			};
		}

		public double calcularValorBasica()
		{
			return 1000;
		}

		public double calcularValorBusiness()
		{
			return 1500;
		}

		public double calcularValorPremium()
		{
			return 2000;
		}
	}
}
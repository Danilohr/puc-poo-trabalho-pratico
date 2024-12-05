using System;
using FlyNow.DTOs;
using FlyNow.Interfaces;

namespace FlyNow.Services
{
    public class ServicoPassagem
    {
        public double CalcularValor(TipoPassagem tipoPassagem, bool ehInternacional)
        {
            double valorBase = tipoPassagem switch
            {
                TipoPassagem.Basica => CalcularValorBasica(),
                TipoPassagem.Business => CalcularValorBusiness(),
                TipoPassagem.Premium => CalcularValorPremium(),
                _ => throw new ArgumentException("Tipo de passagem inválido.")
            };

            if (ehInternacional)
            {
                return valorBase + 100;
            }
            return valorBase;
        }

        public double CalcularValorBasica()
        {
            return 1000;
        }

        public double CalcularValorBusiness()
        {
            return 1500;
        }

        public double CalcularValorPremium()
        {
            return 2000;
        }

        public double CalcularValorBagagem(double valorPrimeiraBagagem, double valorBagagemAdicional)
        {
            return valorPrimeiraBagagem + valorBagagemAdicional;
        }
    }
}
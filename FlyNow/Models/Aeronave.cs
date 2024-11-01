using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Aeronave : _Base
	{
		private int CapacidadePassageiros;
		private double QuantidadeCarga;
		private SortedList<string, string> ListAssentos = new SortedList<string, string>();

		public Aeronave(int capacidadePassageiros, double quantidadeCarga)
		{
			CapacidadePassageiros = capacidadePassageiros;
			QuantidadeCarga = quantidadeCarga;
			geraAssentos();
		}

		private void geraAssentos()
		{
			for (int i = 0, letra = 65; i < 28; i++, letra++)
			{
				string assento = "";

				if (letra == 90)
					letra = 65;

				assento += i + Convert.ToChar(letra);

				ListAssentos.Add(assento, "Disponível");
			}
		}

		public void alteraAssentoParaDisponivel(string assento)
		{
			int keyIndex = ListAssentos.IndexOfKey(assento);
			ListAssentos.SetValueAtIndex(keyIndex, "Disponível");
		}

		public void alteraAssentoParaOcupado(string assento)
		{
			int keyIndex = ListAssentos.IndexOfKey(assento);
			ListAssentos.SetValueAtIndex(keyIndex, "Ocupado");
		}

		public bool verificaOcupacaoAssento(string assento)
		{
			int keyIndex = ListAssentos.IndexOfKey(assento);
			string valueIndex = ListAssentos.GetValueAtIndex(keyIndex);

			if (valueIndex == "Disponível")
				return true;
			
			return false;
		}

		public void exibirAssentos()
		{
			{
				Console.WriteLine("\t-KEY-\t-VALUE-");
				for (int i = 0; i < ListAssentos.Count; i++)
				{
					Console.WriteLine("\t{0}:\t{1}", ListAssentos.GetKeyAtIndex(i), ListAssentos.GetValueAtIndex(i));
				}
				Console.WriteLine();
			}
		}
	}
}

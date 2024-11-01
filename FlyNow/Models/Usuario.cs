using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Usuario : _Base
	{
		private string Login;
		private string Senha;

		public Usuario(string login, string senha)
		{
			Login = login;
			Senha = senha;
		}

		public bool logar()
		{
			string login;
			string senha;

			Console.WriteLine("Informe o seu login");
			login = Console.ReadLine();
			Console.WriteLine("Informe a sua senha");
			senha = Console.ReadLine();

			if (senha != this.Senha || login != this.Login)
				return false;
			else
				return true;
		}
		
		public bool buscarVoo(Aeroporto aeroportoPartida, Aeroporto aeroportoChegada, DateTime date, List<Voo> listVoos)
		{
			foreach (Voo voo in listVoos)
			{
				if(aeroportoPartida == voo.Origem && aeroportoChegada == voo.Destino && date == voo.Data)
					return true;
			}

			return false;
		}

		public bool buscarVoo(Aeroporto aeroportoPartidaIda, Aeroporto aeroportoChegadaIda, Aeroporto aeroportoPartidaVolta, Aeroporto aeroportoChegadaVolta, DateTime dataIda, DateTime dataVolta, List<Voo> listVoos)
		{
			foreach (Voo voo in listVoos)
			{
				if (aeroportoPartidaIda == voo.Origem && aeroportoChegadaIda == voo.Destino && dataIda == voo.Data)
				{
					foreach (Voo voo2 in listVoos)
					{
						if (aeroportoPartidaVolta == voo2.Origem && aeroportoChegadaVolta == voo2.Destino && dataVolta == voo2.Data)
							return true;
					}
				}
					
			}

			return false;
		}

		public void reservarAssento(Voo voo)
		{
			string assento;

			voo.AeronaveOperadora.exibirAssentos();

			Console.WriteLine("informe qual assento deseja");
			assento = Console.ReadLine();

			if (voo.AeronaveOperadora.verificaOcupacaoAssento(assento))
			{
				voo.AeronaveOperadora.alteraAssentoParaOcupado(assento);
				Console.WriteLine("Assento reservado com sucesso!");
			}
			else
				Console.WriteLine("Assento já reservado!");

		}
	}
}

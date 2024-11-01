using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	public class Voo : _Base
	{
		public string CodVoo { get; }
		public Aeroporto Origem { get; }
		public Aeroporto Destino { get; }
		public DateTime Data { get; }
		public CompanhiaAerea CompanhiaOperadora { get; }
		private bool EhFrequente { get;} = false;
		private string DiaFrequencia { get; }
		public Aeronave AeronaveOperadora { get; }
		public bool Status { get; set; } = true;
		public TimeSpan Horario { get; }
		public TimeSpan Duracao { get; }
		private List<DayOfWeek> DiasFrequencia { get; }

		public Voo(Aeroporto origem, Aeroporto destino, DateTime data, TimeSpan horario, TimeSpan duracao, CompanhiaAerea companhiaAerea, Aeronave aeronave)
		{
			CodVoo = geraCodVoo();
			Origem = origem;
			Destino = destino;
			Data = data;
			CompanhiaOperadora = companhiaAerea;
			AeronaveOperadora = aeronave;
			Horario = horario;
			Duracao = duracao;
		}
		public Voo(Aeroporto origem, Aeroporto destino, DateTime data, TimeSpan horario, TimeSpan duracao, CompanhiaAerea companhiaAerea, Aeronave aeronave, bool ehFrequente)
		{
			CodVoo = geraCodVoo();
			Origem = origem;
			Destino = destino;
			Data = data;
			CompanhiaOperadora = companhiaAerea;
			AeronaveOperadora = aeronave;
			EhFrequente = ehFrequente;
			Horario = horario;
			Duracao = duracao;
		}

		private string geraCodVoo()
		{
			Random random = new Random();
			char letra;
			string codVoo = "";

			for (int i = 0; i < 2; i++)
			{
				letra = Convert.ToChar(random.Next(65, 91));
				codVoo += letra;
			}

			for (int i = 0; i < 4; i++)
			{
				codVoo += random.Next(0, 10);
			}

			return codVoo;
		}

		public static List<DateTime> GerarFrequencia(int dias, List<DayOfWeek> diaDaSemana)
		{
			List<DateTime> voosFrequentes = new List<DateTime>();
			DateTime hoje = DateTime.Now;

			for (int i = 0; i < dias; i++)
			{
				DateTime dataVoo = hoje.AddDays(i);
				if (diaDaSemana.Contains(dataVoo.DayOfWeek))
				{				
					voosFrequentes.Add(dataVoo);
				}
			}

			return voosFrequentes;
		}
	}
}

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
		public CompanhiaAerea CompanhiaOperadora {  get; }

		public Voo(string codVoo, Aeroporto origem, Aeroporto destino, DateTime data, CompanhiaAerea companhiaAerea)
		{
			CodVoo = codVoo;
			Origem = origem;
			Destino = destino;
			Data = data;
			CompanhiaOperadora = companhiaAerea;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class Aeroporto : _Base
	{
		private string Nome;
		private string Cidade;
		private string Sigla;
		private string Uf;
		private string Pais;

		public Aeroporto(string nome, string cidade, string sigla, string uf, string pais)
		{
			Nome = nome;
			Cidade = cidade;
			Sigla = sigla;
			Uf = uf;
			Pais = pais;
		}
	}
}

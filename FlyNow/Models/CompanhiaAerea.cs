using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyNow.Models
{
	internal class CompanhiaAerea
	{
		private double Codigo;
		private string Nome;
		private string RazaoSocial;
		private string Cnpj;

		public CompanhiaAerea(double codigo, string nome, string razaoSocial, string cnpj)
		{
			Codigo = codigo;
			Nome = nome;
			RazaoSocial = razaoSocial;
			Cnpj = cnpj;
		}
	}
}

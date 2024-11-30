using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlyNow.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyNow.EfModels;
using FlyNow.Services;
using FlyNow.Data;
using Microsoft.AspNetCore.Mvc;

namespace Tests
{
	[TestClass()]
	public class VooControllerTests
	{
		FlyNowContext context;

		// estes dados já estarão cadastrados no banco
		public void CadastroBasico()
		{
			#region COMPANHIAS AEREAS
			CompanhiaAerea companhia1 = new CompanhiaAerea
			{
				Cod = 5,
				Nome = "companhia1",
				RazaoSocial = "razaosocial1",
				Cnpj = "989327227",
				TaxaRemuneracao = 205
			};
			CompanhiaAerea companhia2 = new CompanhiaAerea
			{
				Cod = 6,
				Nome = "companhia2",
				RazaoSocial = "razaosocial2",
				Cnpj = "989327227",
				TaxaRemuneracao = 104
			};
			#endregion

			#region VOOS DAS COMPANHIAS AEREAS

			Voo voo1 = new Voo
			{
				IdVoo = 1,
				CodVoo = "AB2048",
				Data = new DateTime(2024, 12, 3, 16, 15, 0),
				EhInternacional = 0,
				Duracao = new TimeOnly(1, 32),
				CompanhiaaereaCod = 5,
				AeronaveIdAeronave = 1,
				IdAeroportoOrigem = 1,
				IdAeroportoDestino = 2
			};
			Voo voo2 = new Voo
			{
				IdVoo = 2,
				CodVoo = "AB2142",
				Data = new DateTime(2024, 12, 15, 16, 15, 0),
				EhInternacional = 0,
				Duracao = new TimeOnly(1, 0),
				CompanhiaaereaCod = 5,
				AeronaveIdAeronave = 1,
				IdAeroportoOrigem = 2,
				IdAeroportoDestino = 1
			};
			Voo voo3 = new Voo
			{
				IdVoo = 3,
				CodVoo = "AB2035",
				Data = new DateTime(2024, 12, 14, 16, 15, 0),
				EhInternacional = 0,
				Duracao = new TimeOnly(1, 36),
				CompanhiaaereaCod = 5,
				AeronaveIdAeronave = 3,
				IdAeroportoOrigem = 1,
				IdAeroportoDestino = 2
			};
			Voo voo4 = new Voo
			{
				IdVoo = 4,
				CodVoo = "CB1048",
				Data = new DateTime(2024, 11, 30, 11, 15, 0),
				EhInternacional = 0,
				Duracao = new TimeOnly(3, 12),
				CompanhiaaereaCod = 6,
				AeronaveIdAeronave = 2,
				IdAeroportoOrigem = 3,
				IdAeroportoDestino = 1
			};
			Voo voo5 = new Voo
			{
				IdVoo = 5,
				CodVoo = "OB2048",
				Data = new DateTime(2024, 12, 01, 5, 15, 0),
				EhInternacional = 1,
				Duracao = new TimeOnly(4, 9),
				CompanhiaaereaCod = 6,
				AeronaveIdAeronave = 2,
				IdAeroportoOrigem = 1,
				IdAeroportoDestino = 4
			};
			Voo voo6 = new Voo
			{
				IdVoo = 6,
				CodVoo = "AI2048",
				Data = new DateTime(2024, 12, 2, 18, 15, 0),
				EhInternacional = 1,
				Duracao = new TimeOnly(2, 26),
				CompanhiaaereaCod = 6,
				AeronaveIdAeronave = 2,
				IdAeroportoOrigem = 4,
				IdAeroportoDestino = 2
			};

			#endregion

			#region OUTROS CADASTROS

			Aeroporto aeroporto1 = new Aeroporto
			{
				IdAeroporto = 1,
				Sigla = "GRU",
				Nome = "Aeroporto Internacional de São Paulo",
				Cidade = "São Paulo",
				Uf = "SP"
			};
			Aeroporto aeroporto2 = new Aeroporto
			{
				IdAeroporto = 2,
				Sigla = "GIG",
				Nome = "Aeroporto Internacional do Rio de Janeiro",
				Cidade = "Rio de Janeiro",
				Uf = "RJ"
			};
			Aeroporto aeroporto3 = new Aeroporto
			{
				IdAeroporto = 3,
				Sigla = "CNF",
				Nome = "Aeroporto Internacional Tancredo Neves",
				Cidade = "Belo Horizonte",
				Uf = "MG"
			};
			Aeroporto aeroporto4 = new Aeroporto
			{
				IdAeroporto = 4,
				Sigla = "BSB",
				Nome = "Aeroporto Internacional de Brasília",
				Cidade = "Brasília",
				Uf = "DF"
			};

			#endregion

		}

		[TestMethod()]
		public void Cenario1()
		{
			context = new FlyNowContext();
			PassagemController passagemController = new PassagemController(context);
			VooController vooController = new VooController(context);
			BilhetesController bilhetesController = new BilhetesController(context);

			using (var transaction = context.Database.BeginTransaction())
			{
				// TESTE 1
				var result = vooController.GetVoo(1, 2, new DateTime(2024, 11, 10, 14, 00, 00));
				Assert.IsInstanceOfType(result, typeof(OkObjectResult));

				// TESTE 2
				result = passagemController.AdquirirPassagem();
				Assert.IsInstanceOfType(result, typeof(OkObjectResult));

				// TESTE 3
				result = bilhetesController.RealizarCheckIn(1);
				Assert.IsInstanceOfType(result, typeof(OkObjectResult));

				// TESTE 4
				Assert.IsInstanceOfType(result, CartaoDeEmbarque);

				transaction.Rollback();
			}
		}
	}
}
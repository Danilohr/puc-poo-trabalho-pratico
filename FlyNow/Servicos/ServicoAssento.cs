using System;
using FlyNow.Data;
using FlyNow.DTOs;
using FlyNow.Interfaces;

namespace FlyNow.Services
{
	public class ServicoAssento
	{
		private readonly FlyNowContext db;

		public ServicoAssento(FlyNowContext context)
		{
			db = context;
		}

		public bool VerificarDisponibilidadeAssento(int aeronaveId, int numeroFileira, string letraAssento)
		{
			var assento = db.Assentos
					.FirstOrDefault(a => a.AeronaveIdAeronave == aeronaveId &&
															 a.NumeroFileira == numeroFileira &&
															 a.LetraAssento == letraAssento);

			if (assento == null)
			{
				throw new Exception("Assento não encontrado.");
			}

			// Verifica se o assento está ocupado (0 é livre, 1 é ocupado)
			return assento.Ocupado == 0;
		}

		public void ReservarAssento(int aeronaveId, int numeroFileira, string letraAssento)
		{
			if (!VerificarDisponibilidadeAssento(aeronaveId, numeroFileira, letraAssento))
			{
				throw new Exception("Assento não disponível.");
			}

			var assento = db.Assentos
					.FirstOrDefault(a => a.AeronaveIdAeronave == aeronaveId &&
															 a.NumeroFileira == numeroFileira &&
															 a.LetraAssento == letraAssento);

			if (assento != null)
			{
				assento.Ocupado = 1;
				db.SaveChanges();
			}
			else
			{
				throw new Exception("Erro ao reservar o assento.");
			}
		}
	}
}
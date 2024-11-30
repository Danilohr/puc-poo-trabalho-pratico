using System;
using System.IO;
using FlyNow.Interfaces;
namespace FlyNow.Services
{
	public class ServicoLog : ILog
	{
		private readonly string caminhoArquivo;
		public ServicoLog()
		{
			caminhoArquivo = "Logs/log_acoes.txt";
		}
		public void RegistrarLog(string operacao)
		{
			try
			{
				string mensagem = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {operacao}";
				File.AppendAllText(caminhoArquivo, mensagem + Environment.NewLine);
			}
			catch (Exception ex)
			{
				// Opcional: você pode salvar erros de log em outro arquivo ou lançar a exceção
				Console.WriteLine($"Erro ao registrar log: {ex.Message}");
			}
		}
	}
}
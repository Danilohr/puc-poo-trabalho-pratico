using System.Runtime.Serialization;

namespace FlyNow.Controllers
{
	public enum StatusPassagem
	{
		[EnumMember(Value = "Passagem adquirida")]
		PassagemAdquirida,

		[EnumMember(Value = "Passagem cancelada")]
		PassagemCancelada,

		[EnumMember(Value = "Check-in realizado")]
		CheckInRealizado,

		[EnumMember(Value = "Embarque realizado")]
		EmbarqueRealizado,

		[EnumMember(Value = "NO SHOW")]
		NoShow
	}
}

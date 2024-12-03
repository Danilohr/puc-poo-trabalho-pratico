using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class PassageiroVip
{
	public int CompanhiaaereaCod { get; set; }

	public int PassageiroIdPassageiro { get; set; }

	public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;

	public virtual Passageiro PassageiroIdPassageiroNavigation { get; set; } = null!;
}

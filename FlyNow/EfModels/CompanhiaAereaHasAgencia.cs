using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class CompanhiaaereaHasAgencia
{
    public int CompanhiaaereaCod { get; set; }

    public int AgenciaIdAgencia { get; set; }

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;
}

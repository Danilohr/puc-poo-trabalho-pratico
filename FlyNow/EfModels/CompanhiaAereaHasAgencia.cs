using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class CompanhiaAereaHasAgencia
{
    public double CompanhiaaereaCod { get; set; }

    public int AgenciaIdAgencia { get; set; }

    public int AgenciaFuncionarioIdFuncionario { get; set; }

    public virtual Agencia Agencium { get; set; } = null!;

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;
}

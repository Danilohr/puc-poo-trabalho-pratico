using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Tarifa
{
    public int IdTarifa { get; set; }

    public double? Valor { get; set; }

    public double CompanhiaaereaCod { get; set; }

    public int PassagemIdPassagem { get; set; }

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;

    public virtual Passagem PassagemIdPassagemNavigation { get; set; } = null!;
}

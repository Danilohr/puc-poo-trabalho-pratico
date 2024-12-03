using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Tarifa
{
    public int IdTarifa { get; set; }

    public double? Valor { get; set; }

    public int CompanhiaaereaCod { get; set; }

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;

    public virtual ICollection<Passagem> Passagems { get; set; } = new List<Passagem>();
}

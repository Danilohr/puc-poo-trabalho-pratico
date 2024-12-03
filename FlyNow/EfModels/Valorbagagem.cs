using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Valorbagagem
{
    public int Id { get; set; }

    public double? ValorPrimeiraBagagem { get; set; }

    public double? ValorBagagemAdicional { get; set; }

    public int CompanhiaaereaCod { get; set; }

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;

    public virtual ICollection<Passagem> Passagems { get; set; } = new List<Passagem>();
}

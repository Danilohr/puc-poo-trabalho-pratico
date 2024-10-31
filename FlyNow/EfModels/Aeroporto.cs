using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Aeroporto
{
    public int IdAeroporto { get; set; }

    public string Sigla { get; set; } = null!;

    public string? Nome { get; set; }

    public string? Cidade { get; set; }

    public string? Uf { get; set; }

    public virtual ICollection<Voo> VooIdVoos { get; set; } = new List<Voo>();
}

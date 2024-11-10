using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class CompanhiaAerea
{
    public int Cod { get; set; }

    public string Nome { get; set; } = null!;

    public string RazaoSocial { get; set; } = null!;

    public string Cnpj { get; set; } = null!;

    public double? TaxaRemuneracao { get; set; }

    public virtual ICollection<CompanhiaaereaHasAgencium> CompanhiaaereaHasAgencia { get; set; } = new List<CompanhiaaereaHasAgencium>();

    public virtual ICollection<Tarifa> Tarifas { get; set; } = new List<Tarifa>();

    public virtual Valorbagagem? Valorbagagem { get; set; }

    public virtual ICollection<Voo> Voos { get; set; } = new List<Voo>();
}

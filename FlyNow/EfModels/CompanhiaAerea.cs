using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class CompanhiaAerea
{
    public double Cod { get; set; }

    public string? Nome { get; set; }

    public string? RazaoSocial { get; set; }

    public string? Cnpj { get; set; }

    public string? TipoVoo { get; set; }

    public double? TaxaRemuneracao { get; set; }

    public virtual CompanhiaAereaHasAgencia? CompanhiaaereaHasAgencium { get; set; }

    public virtual ICollection<Tarifa> Tarifas { get; set; } = new List<Tarifa>();

    public virtual ICollection<Valorbagagem> Valorbagagems { get; set; } = new List<Valorbagagem>();

    public virtual ICollection<Voo> Voos { get; set; } = new List<Voo>();
}

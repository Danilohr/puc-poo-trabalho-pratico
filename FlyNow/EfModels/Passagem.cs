using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Passagem
{
    public int IdPassagem { get; set; }

    public string? Moeda { get; set; }

    public string? Tarifa { get; set; }

    public string? CompanhiaAerea { get; set; }

    public int BilheteIdBilhete { get; set; }

    public int ValorbagagemId { get; set; }

    public virtual Bilhete BilheteIdBilheteNavigation { get; set; } = null!;

    public virtual ICollection<Tarifa> Tarifas { get; set; } = new List<Tarifa>();

    public virtual Valorbagagem Valorbagagem { get; set; } = null!;

    public virtual ICollection<Voo> VooIdVoos { get; set; } = new List<Voo>();
}

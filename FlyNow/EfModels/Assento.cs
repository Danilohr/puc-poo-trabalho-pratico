using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Assento
{
    public int IdAssento { get; set; }

    public int? NumeroFileira { get; set; }

    public string? LetraAssento { get; set; }

    public int AeronaveIdAeronave { get; set; }

    public sbyte? Ocupado { get; set; }

    public virtual Aeronave AeronaveIdAeronaveNavigation { get; set; } = null!;
}

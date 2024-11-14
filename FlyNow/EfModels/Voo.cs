using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Voo
{
    public int IdVoo { get; set; }

    public string? CodVoo { get; set; }

    public DateTime Data { get; set; }

    public sbyte? EhInternacional { get; set; }

    public TimeOnly? Duracao { get; set; }

    public int CompanhiaaereaCod { get; set; }

    public int AeronaveIdAeronave { get; set; }

    public int IdAeroportoOrigem { get; set; }

    public int IdAeroportoDestino { get; set; }

    public virtual Aeronave AeronaveIdAeronaveNavigation { get; set; } = null!;

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;

    public virtual Aeroporto IdAeroportoDestinoNavigation { get; set; } = null!;

    public virtual Aeroporto IdAeroportoOrigemNavigation { get; set; } = null!;

    public virtual Passagem? PassagemIdVoo1Navigation { get; set; }

    public virtual ICollection<Passagem> PassagemIdVoo2Navigations { get; set; } = new List<Passagem>();
}

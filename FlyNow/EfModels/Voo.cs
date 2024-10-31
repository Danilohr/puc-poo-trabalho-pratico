using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Voo
{
    public int IdVoo { get; set; }

    public string? CodVoo { get; set; }

    public DateTime? Data { get; set; }

    public string? Destino { get; set; }

    public string? Origem { get; set; }

    public double CompanhiaaereaCod { get; set; }

    public virtual CompanhiaAerea CompanhiaaereaCodNavigation { get; set; } = null!;

    public virtual ICollection<Aeroporto> AeroportoIdAeroportos { get; set; } = new List<Aeroporto>();

    public virtual ICollection<Passagem> PassagemIdPassagems { get; set; } = new List<Passagem>();
}

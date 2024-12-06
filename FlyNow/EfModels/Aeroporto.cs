using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Aeroporto
{
  public int IdAeroporto { get; set; }

  public string? Sigla { get; set; }

  public string? Nome { get; set; }

  public string? Cidade { get; set; }

  public string? Uf { get; set; }

	public float Latitude { get; set; }

	public float Longitude { get; set; }


	public virtual ICollection<Voo> VooIdAeroportoDestinoNavigations { get; set; } = new List<Voo>();

  public virtual ICollection<Voo> VooIdAeroportoOrigemNavigations { get; set; } = new List<Voo>();
}

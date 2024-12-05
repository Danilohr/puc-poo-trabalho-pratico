using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Aeronave
{
  public int IdAeronave { get; set; }

	public string? Nome { get; set; }

  public int CapacidadePassageiros { get; set; }

  public int CapacidadeBagagens { get; set; }

	public int NumeroFileiras { get; set; }
	
	public int AssentosPorFileira { get; set; }

	public virtual ICollection<Assento> Assentos { get; set; } = new List<Assento>();

  public virtual ICollection<Voo> Voos { get; set; } = new List<Voo>();
}

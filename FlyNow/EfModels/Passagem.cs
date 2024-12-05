using FlyNow.DTOs;
using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Passagem
{
  public int IdPassagem { get; set; }

  public string? Moeda { get; set; }

  public int TarifaIdTarifa { get; set; }

  public int ValorbagagemId { get; set; }

  public int IdVoo1 { get; set; }

  public int? IdVoo2 { get; set; }

	public double? ValorPassagem { get; set; }

	public int CompanhiaAereaId { get; set; }

	public StatusPassagemDto Status { get; set; }

	public virtual ICollection<Bilhete> Bilhetes { get; set; } = new List<Bilhete>();

  public virtual Voo Voo1 { get; set; } = null!;

  public virtual Voo? Voo2 { get; set; }

  public virtual Tarifa Tarifa { get; set; } = null!;

  public virtual Valorbagagem Valorbagagem { get; set; } = null!;
}

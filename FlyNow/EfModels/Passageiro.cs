using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Passageiro
{
  public int IdPassageiro { get; set; }

  public string? Nome { get; set; }

  public string? Cpf { get; set; }

	public string? Email { get; set; }	

	public string? Rg { get; set; }

  public int UsuarioIdUsuario { get; set; }

	public string? Passaporte { get; set; }

	public virtual ICollection<Bilhete> Bilhetes { get; set; } = new List<Bilhete>();

	public virtual Usuario UsuarioIdUsuarioNavigation { get; set; } = null!;
}

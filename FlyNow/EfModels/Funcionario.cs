using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlyNow.EfModels;

public partial class Funcionario
{
	public int IdFuncionario { get; set; }

  public string? Cpf { get; set; }

  public string? Nome { get; set; }

  public string? Email { get; set; }

  public virtual ICollection<Agencia> Agencia { get; set; } = new List<Agencia>();

  public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

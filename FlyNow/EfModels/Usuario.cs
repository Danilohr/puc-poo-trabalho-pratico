using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string? Login { get; set; }

    public string? Senha { get; set; }

    public int FuncionarioIdFuncionario { get; set; }

    public virtual Funcionario FuncionarioIdFuncionarioNavigation { get; set; } = null!;

    public virtual ICollection<Passageiro> Passageiros { get; set; } = new List<Passageiro>();
}

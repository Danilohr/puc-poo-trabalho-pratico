using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Funcionario
{
    public int IdFuncionario { get; set; }

    public string? Cpf { get; set; }

    public string? Nome { get; set; }

    public string? Email { get; set; }

    public virtual ICollection<Agencium> Agencia { get; set; } = new List<Agencium>();

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}

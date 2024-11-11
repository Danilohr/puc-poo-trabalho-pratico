using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Agencia
{
    public int IdAgencia { get; set; }

    public string? Nome { get; set; }

    public double? TaxaAgencia { get; set; }

    public int FuncionarioIdFuncionario { get; set; }

    public virtual Funcionario Funcionario { get; set; } = null!;
}

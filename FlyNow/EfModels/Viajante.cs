using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Viajante
{
    public int IdViajante { get; set; }

    public string? Nome { get; set; }

    public string? Cpf { get; set; }

    public string? Rg { get; set; }

    public int BilheteIdBilhete { get; set; }

    public int UsuarioIdUsuario { get; set; }

    public virtual Bilhete BilheteIdBilheteNavigation { get; set; } = null!;

    public virtual Usuario UsuarioIdUsuarioNavigation { get; set; } = null!;
}

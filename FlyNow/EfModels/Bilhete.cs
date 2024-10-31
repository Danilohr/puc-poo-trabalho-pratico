using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Bilhete
{
    public int IdBilhete { get; set; }

    public sbyte? BilheteInternacional { get; set; }

    public virtual ICollection<Passagem> Passagems { get; set; } = new List<Passagem>();

    public virtual Viajante? Viajante { get; set; }
}

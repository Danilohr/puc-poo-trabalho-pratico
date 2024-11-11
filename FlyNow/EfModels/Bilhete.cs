﻿using System;
using System.Collections.Generic;

namespace FlyNow.EfModels;

public partial class Bilhete
{
    public int PassagemIdPassagem { get; set; }

    public int PassageiroIdPassageiro { get; set; }

    public sbyte? BilheteInternacional { get; set; }

    public string? StatusPassageiro { get; set; }

    public virtual Passageiro Passageiro { get; set; } = null!;

    public virtual Passagem Passagem { get; set; } = null!;
}

using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class ValorBagagemDto
{
	public double? ValorPrimeiraBagagem { get; set; }

	public double? ValorBagagemAdicional { get; set; }

	public int CompanhiaaereaCod { get; set; }
}

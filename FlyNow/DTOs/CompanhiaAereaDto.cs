using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class CompanhiaAereaDto
{
	public string Nome { get; set; } = null!;

	public string RazaoSocial { get; set; } = null!;

	public string Cnpj { get; set; } = null!;

	public double? TaxaRemuneracao { get; set; }
}

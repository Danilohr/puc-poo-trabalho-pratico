using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.Data;

public class AeroportoDto
{
	public string? Sigla { get; set; }

	public string? Nome { get; set; }

	public string? Cidade { get; set; }

	public string? Uf { get; set; }
}

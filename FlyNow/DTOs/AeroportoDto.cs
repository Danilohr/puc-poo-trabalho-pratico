using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class AeroportoDto
{
	public string? Sigla { get; set; }

	public string? Nome { get; set; }

	public string? Cidade { get; set; }

	public string? Uf { get; set; }

	public double Latitude { get; set; }

	public double Longitude { get; set; }
}

using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class AeronaveDto
{
	public string? Nome { get; set; }
	
	public int CapacidadePassageiros { get; set; }
	
	public int CapacidadeBagagens { get; set; }
}

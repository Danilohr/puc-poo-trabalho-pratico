using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class PassagemDto
{
	public int ValorBagagemId { get; set; }
	
	public int IdVoo1 { get; set; }
	
	public int IdVoo2 { get; set; }
	
	public int CompanhiaAereaCod { get; set; }

	public StatusPassagemDto Status { get; set; }

	public TipoPassagem TipoPassagem { get; set; } // 1 = Básica, 2 = Business, 3 = Premium 
}
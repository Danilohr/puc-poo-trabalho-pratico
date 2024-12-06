using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class EmitirBilheteDto
{
	public int PassageiroId { get; set; }
	
	public int PassagemId { get; set; }
	
	public sbyte? BilheteInternacional { get; set; } // 0 = Nacional, 1 = Internacional
	
	public string StatusPassageiro { get; set; }
	
	public string TipoDocumento { get; set; } // "RG", "CPF", ou "Passaporte"
}
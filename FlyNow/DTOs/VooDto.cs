using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class VooDto
{
	public string CodVoo { get; set; }
	
	public DateTime Data { get; set; }
	
	public sbyte? EhInternacional { get; set; }
	
	public TimeOnly? Duracao { get; set; }
	
	public int CompanhiaaereaCod { get; set; }
	
	public int AeronaveIdAeronave { get; set; }
	
	public int IdAeroportoOrigem { get; set; }
	
	public int IdAeroportoDestino { get; set; }
}

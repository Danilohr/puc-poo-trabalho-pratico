using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.Data;

public class UsuarioDto
{
	public string? Login { get; set; }
	public string? Senha { get; set; }
}

using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.Data;

public class FuncionarioDto
{
	public string? Nome { get; set; }
	public string? Cpf { get; set; }
	public string? Email { get; set; }
}

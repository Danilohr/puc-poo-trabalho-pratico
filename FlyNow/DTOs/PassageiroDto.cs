using System;
using System.Collections.Generic;
using FlyNow.EfModels;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FlyNow.DTOs;

public class PassageiroDto
{
	public string? Nome { get; set; }

	public string? Cpf { get; set; }

	public string? Email { get; set; }

	public string? Rg { get; set; }

	public int UsuarioIdUsuario { get; set; }
}

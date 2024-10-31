using Microsoft.EntityFrameworkCore;
using FlyNow.Infrastructure;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

void ConfigureServices(WebApplicationBuilder builder)
{
	builder.Services.AddControllers();

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();

	var con = builder.Configuration.GetConnectionString("ConexaoMySql");
	builder.Services.AddDbContext<ProgramDbContext>( options => {
		options.UseMySql(con, ServerVersion.AutoDetect(con)); 
		}
	);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
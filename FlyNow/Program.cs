using Microsoft.EntityFrameworkCore;
using FlyNow.Data;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var con = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<FlyNowContext>(options =>
{
	options.UseMySql(con, ServerVersion.AutoDetect(con));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapDefaultControllerRoute();

app.Run();
using API_Interna.Business;
using API_Interna.Controllers;
using API_Interna.Data;
using API_Interna.Interfaces.Business;
using API_Interna.Interfaces.Repositorio;
using API_Interna.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DPContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DPConnection"))
); //Configura a injeção de dependência

builder.Services.AddScoped<IOSCRepository, OSCRepository>();
builder.Services.AddScoped<IOSCBusiness, OSCBusiness>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

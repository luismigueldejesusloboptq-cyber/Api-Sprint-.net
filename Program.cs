using Api_Lanchonete_Sprint.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ConexaoPadrao");
builder.Services.AddDbContext<LanchoneteContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Força a internet a usar HTTPS (mais seguro)
app.UseHttpsRedirection();

// Prepara o sistema de autorização (senhas/tokens)
app.UseAuthorization();

// Mapeia as rotas (URLs) para os nossos Controllers
app.MapControllers();

// Roda a aplicação!
app.Run();
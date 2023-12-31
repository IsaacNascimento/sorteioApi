using Microsoft.EntityFrameworkCore;
using Src.Connection;
using Src.Pessoa.Helpers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<SorteioDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("Postgresql")));
builder.Services.AddControllers();
builder.Services.AddScoped<PessoaHelpers>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Routing

app.MapControllers();

app.Run();


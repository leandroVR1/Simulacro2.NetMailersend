using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Simulacro2.Data;
using Simulacro2.Interfaces;
using Simulacro2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
void AddCustomDbContext<TContext>(string connectionString) where TContext : DbContext
{
    builder.Services.AddDbContext<TContext>(options =>
        options.UseMySql(
            connectionString,
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")
        )
    );
}

AddCustomDbContext<BaseContext>(builder.Configuration.GetConnectionString("MySqlConnection"));

// Registro de servicios
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();

builder.Services.AddScoped<EspecialidadService>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();


// Configuración de los controladores
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Libros API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

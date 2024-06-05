using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Simulacro2.Data;
using Simulacro2.Interfaces;
using Simulacro2.Services;

var builder = WebApplication.CreateBuilder(args);

// Añadir servicios al contenedor.
void AddCustomDbContext<TContext>(string connectionString) where TContext : DbContext
{
    // Utilizar la inyección de dependencias para añadir un DbContext al contenedor de servicios.
    // El contexto está configurado para utilizar MySQL como proveedor de base de datos.
    builder.Services.AddDbContext<TContext>(options =>
        options.UseMySql(
            connectionString,
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.20-mysql")
        )
    );
}

// Configurar el contexto de la base de datos.
AddCustomDbContext<BaseContext>(builder.Configuration.GetConnectionString("MySqlConnection"));

// Registrar servicios.
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();

builder.Services.AddScoped<EspecialidadService>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();

builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<IPacienteService, PacienteService>();

builder.Services.AddScoped<CitaService>();
builder.Services.AddScoped<ICitaService, CitaService>();

builder.Services.AddScoped<TratamientoService>();
builder.Services.AddScoped<ITratamientoService, TratamientoService>();

// Configurar controladores y opciones JSON.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    // Configurar opciones de serialización JSON.
    // Ignorar valores nulos al escribir JSON.
    // Utilizar un convertidor personalizado para manejar enums como cadenas.
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Configurar Swagger para la documentación de la API.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP.
if (app.Environment.IsDevelopment())
{
    // En el entorno de desarrollo, usar Swagger para la documentación de la API.
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
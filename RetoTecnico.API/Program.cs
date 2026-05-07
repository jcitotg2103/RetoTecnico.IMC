using Azure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RetoTecnico.API.Middlewares;
using RetoTecnico.Application.Interfaces;
using RetoTecnico.Application.Services;
using RetoTecnico.Infrastructure.Persistence;
using RetoTecnico.Infrastructure.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// CONFIGURACIÓN AZURE KEY VAULT 
var keyVaultUrl = builder.Configuration["KeyVaultUrl"];
if (!string.IsNullOrEmpty(keyVaultUrl))
{
    // Conexion a Azure y lee la cadena de conexión
    builder.Configuration.AddAzureKeyVault(
        new Uri(keyVaultUrl),
        new DefaultAzureCredential());
}

// Obtenemos la cadena de conexión 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// CONFIGURACIÓN BASE DE DATOS
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// INYECCIÓN DE DEPENDENCIAS 
builder.Services.AddScoped<IEvaluacionRepository, EvaluacionRepository>();
builder.Services.AddScoped<IRangoRepository, RangoRepository>();
builder.Services.AddScoped<ImcService>();

// CONFIGURACIÓN SEGURIDAD JWT
var jwtKey = builder.Configuration["Jwt:Key"] ?? "UnaClaveSecretaMuyLargaParaElRetoDeContinental2026!";
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Definimos el esquema de seguridad para la interfaz de Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorización JWT usando el esquema Bearer. \r\n\r\n Ingresa 'Bearer' [espacio] y luego tu token en la caja de abajo.\r\n\r\nEjemplo: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    // Le decimos a Swagger que todos los endpoints requieren este esquema
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// MIDDLEWARE DE ERRORES GLOBAL
app.UseMiddleware<ErrorHandlerMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
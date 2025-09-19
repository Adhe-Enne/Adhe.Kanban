using FluentValidation;
// Agrega el using necesario para la extensión de validación automática de FluentValidation
using FluentValidation.AspNetCore;
using Kanban.Business;
using Kanban.DatabaseContext;
using Kanban.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using technical_tests_backend_ssr.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("La cadena de conexión 'DefaultConnection' no está configurada.");
}

builder.Services.AddContext(connectionString);
builder.Services.AddRepository();
builder.Services.AddServices();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization();

//builder.Services.AddValidatorsFromAssembly(assembly: Assembly.GetExecutingAssembly());
builder.Services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TaskRequestValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RoleRequestValidator>();

builder.Services.AddFluentValidationAutoValidation();
var allowedOrigin = builder.Configuration.GetValue<string>("allowedOrigins")!;

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(configuracion =>
    {
        configuracion.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod();
    });

    opciones.AddPolicy("free", configuracion =>
    {
        configuracion.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("Application started");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PruebaLafise.Application;
using PruebaLafise.Application.Services;
using PruebaLafise.Domain.Interfaces;
using PruebaLafise.Infraestructure.Data;
using PruebaLafise.Infraestructure.Repository;

var builder = WebApplication.CreateBuilder(args);

//Configuracion de DbContext
builder.Services.AddDbContext<BackendDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("ConexionSqlLite") ??
        throw new InvalidOperationException("Cadena de conexion no valida")));

builder.Services.AddScoped<IApplicationDbContext>(provider =>
    provider.GetRequiredService<BackendDbContext>());

//Realizamos la inyeccion de dependencias para la aplicacion
builder.Services.AddTransient<IUserProfileRepository, UserProfileRepository>();
builder.Services.AddTransient<IBankAccountRepository, BankAccountRepository>();
builder.Services.AddTransient<IUserProfileService, UserProfileService>();
builder.Services.AddTransient<IBankAccountService, BankAccountService>();
builder.Services.AddTransient<ICatalogRepository, CatalogRepository>();
builder.Services.AddTransient<ICatalogService, CatalogService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Prueba API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

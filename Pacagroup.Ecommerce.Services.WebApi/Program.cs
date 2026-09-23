using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Application.Main;

var builder = WebApplication.CreateBuilder(args);

// ============================================================
// CONFIGURACIÓN DE SERVICIOS
// ============================================================

// Registra los Controllers de ASP.NET Core.
// Permite que la aplicación pueda recibir y procesar
// solicitudes HTTP mediante los controladores.
builder.Services.AddControllers();

// Registra OpenAPI para generar la documentación
// de los endpoints de la API.
builder.Services.AddOpenApi();

// Registra las dependencias de la capa Domain.
//
// Por ejemplo:
// ICustomersDomain → CustomersDomain
builder.Services.AddDomainServices();

// Registra las dependencias de la capa Infrastructure.
//
// Por ejemplo:
// ICustomersRepository → CustomersRepository
// IUnitOfWork           → UnitOfWork
// DapperContext         → DapperContext
builder.Services.AddInfrastructureServices();

// Registra las dependencias de la capa Application.
//
// Por ejemplo:
// ICustomersApplication → CustomersApplication
// Además, registra AutoMapper.
builder.Services.AddApplicationServices();


// ============================================================
// CONSTRUCCIÓN DE LA APLICACIÓN
// ============================================================

// Construye la aplicación utilizando todos los servicios
// y configuraciones registradas anteriormente.
var app = builder.Build();


// ============================================================
// CONFIGURACIÓN DEL PIPELINE HTTP
// ============================================================

if (app.Environment.IsDevelopment())
{
    // Expone la documentación OpenAPI cuando la aplicación
    // se está ejecutando en entorno de desarrollo.
    app.MapOpenApi();
}

// Redirige las solicitudes HTTP hacia HTTPS.
app.UseHttpsRedirection();

// Activa el middleware de autorización.
app.UseAuthorization();

// Indica que los Controllers deben utilizarse como endpoints
// de la aplicación.
app.MapControllers();

// Inicia la aplicación y comienza a escuchar solicitudes HTTP.
app.Run();
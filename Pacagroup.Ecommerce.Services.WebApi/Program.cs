using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;

var builder = WebApplication.CreateBuilder(args);


// ============================================================
// CONFIGURACIÓN DE SERVICIOS
// ============================================================

// Registra los Controllers de ASP.NET Core.
//
// Permite que la aplicación pueda recibir y procesar
// solicitudes HTTP mediante los Controllers.
builder.Services.AddControllers();


// ============================================================
// CONFIGURACIÓN DE CORS
// ============================================================

// CORS (Cross-Origin Resource Sharing) permite controlar
// qué aplicaciones externas pueden consumir nuestra API.
builder.Services.AddCors(options =>
{
    // Crea una política llamada "MiPoliticaCors".
    options.AddPolicy("MiPoliticaCors", policy =>
    {
        policy
            // Permite solicitudes desde cualquier origen.
            .AllowAnyOrigin()

            // Permite cualquier encabezado HTTP.
            .AllowAnyHeader()

            // Permite cualquier método HTTP:
            // GET, POST, PUT, DELETE, etc.
            .AllowAnyMethod();
    });
});


// ============================================================
// CONFIGURACIÓN DE OPENAPI
// ============================================================

// Registra OpenAPI para generar la documentación
// de los endpoints de la API.
builder.Services.AddOpenApi();


// ============================================================
// CONFIGURACIÓN DE DOMAIN
// ============================================================

// Registra las dependencias de la capa Domain.
//
// Ejemplo:
//
// ICustomersDomain → CustomersDomain
builder.Services.AddDomainServices();


// ============================================================
// CONFIGURACIÓN DE INFRASTRUCTURE
// ============================================================

// Registra las dependencias de la capa Infrastructure.
//
// Ejemplos:
//
// ICustomersRepository → CustomersRepository
// IUnitOfWork           → UnitOfWork
// DapperContext         → DapperContext
builder.Services.AddInfrastructureServices();


// ============================================================
// CONFIGURACIÓN DE APPLICATION
// ============================================================

// Registra las dependencias de la capa Application.
//
// Ejemplo:
//
// ICustomersApplication → CustomersApplication
//
// También registra AutoMapper y sus perfiles de mapeo.
builder.Services.AddApplicationServices();


// ============================================================
// CONFIGURACIÓN DE SWAGGER
// ============================================================

// Registra los servicios necesarios para utilizar Swagger
// y generar la documentación de la API.
builder.Services.AddSwagger();


// ============================================================
// CONSTRUCCIÓN DE LA APLICACIÓN
// ============================================================

// Construye la aplicación utilizando todos los servicios
// y configuraciones registrados anteriormente.
var app = builder.Build();


// ============================================================
// CONFIGURACIÓN DEL PIPELINE HTTP
// ============================================================

if (app.Environment.IsDevelopment())
{
    // Habilita Swagger para generar el documento
    // de descripción de los endpoints de la API.
    app.UseSwagger();

    // Habilita Swagger UI, una interfaz gráfica que permite
    // visualizar y probar los endpoints de la API desde el navegador.
    app.UseSwaggerUI(c =>
    {
        // Indica la ubicación del documento JSON
        // generado por Swagger.
        c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "v1");

        // Define la ruta donde estará disponible Swagger UI.
        //
        // Ejemplo:
        // https://localhost:5001/swagger
        c.RoutePrefix = "swagger";

        // Muestra cuánto tiempo tardó cada petición.
        c.DisplayRequestDuration();

        // Permite navegar directamente hacia operaciones
        // específicas mediante enlaces.
        c.EnableDeepLinking();

        // Muestra extensiones adicionales definidas
        // en la documentación OpenAPI.
        c.ShowExtensions();
    });

    /*
     * Ejemplo utilizando OpenAPI directamente:
     *
     * app.MapOpenApi();
     *
     * En este proyecto no se utiliza porque actualmente
     * estamos utilizando Swagger + Swagger UI.
     */
}


// ============================================================
// MIDDLEWARES DEL PIPELINE HTTP
// ============================================================

// Redirige automáticamente las solicitudes HTTP hacia HTTPS.
app.UseHttpsRedirection();


// Aplica la política CORS que registramos anteriormente.
//
// Debe utilizar exactamente el mismo nombre:
//
// "MiPoliticaCors"
//
// La política permite cualquier origen, encabezado y método
// según la configuración realizada anteriormente.
app.UseCors("MiPoliticaCors");


// Activa el middleware de autorización.
//
// Se encarga de procesar las reglas de autorización
// cuando existen recursos protegidos.
app.UseAuthorization();


// Indica que los Controllers registrados anteriormente
// serán utilizados como endpoints de la API.
app.MapControllers();


// ============================================================
// INICIO DE LA APLICACIÓN
// ============================================================

// Inicia la aplicación y comienza a escuchar
// las solicitudes HTTP.
app.Run();
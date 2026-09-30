using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using System.Reflection;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    /// <summary>
    /// Contiene métodos de extensión relacionados con la configuración
    /// y documentación de Swagger/OpenAPI.
    /// </summary>
    public static class SwaggerExtensions
    {
        /// <summary>
        /// Registra y configura Swagger dentro del contenedor
        /// de inyección de dependencias de ASP.NET Core.
        ///
        /// Al ser un método de extensión, permite utilizarlo
        /// directamente sobre IServiceCollection:
        ///
        /// builder.Services.AddSwagger();
        /// </summary>
        /// <param name="services">
        /// Colección de servicios de ASP.NET Core donde se registrará
        /// la configuración de Swagger.
        /// </param>
        /// <returns>
        /// La misma colección de servicios para continuar
        /// configurando la aplicación.
        /// </returns>
        public static IServiceCollection AddSwagger(
            this IServiceCollection services)
        {
            // Registra Swagger Generator.
            //
            // Se encargará de generar automáticamente el documento
            // OpenAPI a partir de los Controllers y endpoints
            // de nuestra aplicación.
            services.AddSwaggerGen(c =>
            {
                // Define la información principal de la API
                // que aparecerá en la documentación de Swagger.
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    // Versión de la documentación de la API.
                    Version = "v1",

                    // Nombre que aparecerá como título en Swagger UI.
                    Title = "Pacagroup Technology Services API Market",

                    // Descripción general de la API.
                    Description = "A simple example ASP.NET Core Web API. ",

                    // URL donde se encuentran los términos
                    // de servicio de la API.
                    TermsOfService = new Uri(
                        "https://pacagroup.com/terms"),

                    // Información de contacto del responsable
                    // de la API.
                    Contact = new OpenApiContact
                    {
                        Name = "Alex Espejo",
                        Email = "alex.espejo.c@gmail.com",
                        Url = new Uri(
                            "https://pacagroup.com/contact")
                    },

                    // Información relacionada con la licencia
                    // de uso de la API.
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri(
                            "https://pacagroup.com/licence")
                    }
                });


                // =========================================================
                // DOCUMENTACIÓN XML
                // =========================================================

                // Obtiene el nombre del ensamblado actual
                // y le agrega la extensión ".xml".
                //
                // Ejemplo:
                //
                // Pacagroup.Ecommerce.Services.WebApi.xml
                //
                // Este archivo contiene la documentación generada
                // a partir de los comentarios XML del código.
                var xmlFile =
                    $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";


                // Construye la ruta física donde se encuentra
                // el archivo XML generado.
                //
                // AppContext.BaseDirectory representa la carpeta
                // donde se está ejecutando la aplicación.
                var xmlPath = Path.Combine(
                    AppContext.BaseDirectory,
                    xmlFile);


                // Indica a Swagger que incluya los comentarios XML
                // de nuestro código dentro de la documentación.
                //
                // Esto permite que los comentarios:
                //
                // /// <summary>
                //
                // de Controllers, métodos, parámetros, etc.,
                // puedan aparecer en Swagger UI.
                c.IncludeXmlComments(xmlPath);


                // =========================================================
                // CONFIGURACIÓN DE AUTENTICACIÓN JWT EN SWAGGER
                // =========================================================

                // Define cómo Swagger debe solicitar y enviar
                // el token JWT.
                //
                // Esta configuración NO genera el JWT.
                //
                // El JWT lo genera JwtService.
                //
                // Aquí solamente le enseñamos a Swagger:
                // "Existe una autenticación JWT y el token
                // debe enviarse mediante el header Authorization".
                var securityScheme = new OpenApiSecurityScheme
                {
                    // Nombre del header HTTP donde se enviará el token.
                    //
                    // Resultado:
                    //
                    // Authorization: Bearer <token>
                    Name = "Authorization",

                    // Texto que Swagger mostrará al usuario
                    // explicando qué debe introducir.
                    Description = "Enter JWT Bearer token **_only_**",

                    // Indica que el token se enviará
                    // dentro de los headers HTTP.
                    In = ParameterLocation.Header,

                    // Indica que estamos utilizando
                    // un esquema HTTP de autenticación.
                    Type = SecuritySchemeType.Http,

                    // Especifica que utilizaremos el esquema Bearer.
                    //
                    // Bearer es la forma habitual de enviar
                    // un JWT mediante el header Authorization.
                    Scheme = "bearer",

                    // Indica que el formato del token es JWT.
                    BearerFormat = "JWT"
                };


                // Registra el esquema de seguridad en Swagger.
                //
                // JwtBearerDefaults.AuthenticationScheme
                // normalmente contiene el nombre:
                //
                // "Bearer"
                //
                // Esto permite identificar esta configuración
                // de autenticación dentro de Swagger.
                c.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    securityScheme);


                // Indica que los endpoints de la API pueden utilizar
                // el esquema de autenticación JWT definido anteriormente.
                //
                // En otras palabras:
                //
                // AddSecurityDefinition
                //      ↓
                // "Así funciona mi autenticación JWT"
                //
                // AddSecurityRequirement
                //      ↓
                // "Esta autenticación puede ser requerida
                //  para acceder a los endpoints"
                c.AddSecurityRequirement(document =>
                    new OpenApiSecurityRequirement
                    {
                        {
                            // Referencia al esquema de seguridad
                            // que registramos anteriormente.
                            new OpenApiSecuritySchemeReference(
                                JwtBearerDefaults.AuthenticationScheme,
                                document,
                                null),

                            // Lista de scopes.
                            //
                            // En este caso no utilizamos OAuth2 scopes,
                            // por eso queda vacía.
                            []
                        }
                    });


                // Habilita el uso de anotaciones de Swagger/OpenAPI.
                //
                // Permite utilizar atributos como:
                //
                // [SwaggerTag(...)]
                // [SwaggerOperation(...)]
                //
                // para personalizar la documentación
                // de Controllers y endpoints.
                c.EnableAnnotations();
            });


            // Devuelve la colección de servicios.
            //
            // Esto permite continuar encadenando configuraciones
            // durante el registro de dependencias.
            return services;
        }
    }
}
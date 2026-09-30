using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication
{
    /// <summary>
    /// Contiene métodos de extensión relacionados con
    /// la configuración de autenticación de la API.
    /// </summary>
    public static class AuthenticationExtensions
    {
        /// <summary>
        /// Registra y configura la autenticación JWT
        /// dentro del contenedor de dependencias.
        ///
        /// Permite que ASP.NET Core pueda validar los JWT
        /// enviados en las peticiones HTTP.
        /// </summary>
        /// <param name="services">
        /// Colección de servicios donde se registra
        /// la configuración de autenticación.
        /// </param>
        /// <param name="configuration">
        /// Configuración de la aplicación utilizada para
        /// obtener los valores de la sección Jwt.
        /// </param>
        /// <returns>
        /// La misma colección de servicios para continuar
        /// configurando la aplicación.
        /// </returns>
        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Obtiene la sección "Jwt" de la configuración.
            //
            // Ejemplo:
            //
            // "Jwt": {
            //     "Key": "...",
            //     "Issuer": "...",
            //     "Audience": "..."
            // }
            //
            // A partir de esta sección podremos obtener
            // cada uno de esos valores.
            var jwtSettings = configuration.GetSection("Jwt");


            // Registra el sistema de autenticación de ASP.NET Core.
            //
            // JwtBearerDefaults.AuthenticationScheme indica
            // que el esquema de autenticación utilizado será Bearer.
            //
            // Es decir, ASP.NET Core espera recibir el JWT
            // normalmente mediante:
            //
            // Authorization: Bearer <token>
            services.AddAuthentication(
                JwtBearerDefaults.AuthenticationScheme)

                // Configura específicamente cómo ASP.NET Core
                // debe procesar y validar los tokens JWT.
                .AddJwtBearer(options =>
                {
                    // Define las reglas que debe cumplir un JWT
                    // para considerarse válido.
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            // Comprueba que el "issuer" del JWT
                            // coincida con el Issuer configurado.
                            //
                            // Issuer = quién emitió el token.
                            ValidateIssuer = true,


                            // Comprueba que el "audience" del JWT
                            // coincida con el Audience configurado.
                            //
                            // Audience = para quién está destinado
                            // el token.
                            ValidateAudience = true,


                            // Comprueba que el token no haya expirado.
                            //
                            // Utiliza el valor "exp" incluido
                            // dentro del JWT.
                            ValidateLifetime = true,


                            // Comprueba que la firma del JWT
                            // sea válida utilizando la clave configurada.
                            //
                            // Esto permite detectar si el token
                            // fue alterado o firmado con otra clave.
                            ValidateIssuerSigningKey = true,


                            // Valor esperado del Issuer.
                            //
                            // Debe coincidir con el valor utilizado
                            // cuando JwtService genera el token.
                            ValidIssuer = jwtSettings["Issuer"],


                            // Valor esperado del Audience.
                            //
                            // Debe coincidir con el valor utilizado
                            // cuando JwtService genera el token.
                            ValidAudience = jwtSettings["Audience"],


                            // Clave utilizada para comprobar la firma
                            // del JWT.
                            //
                            // Debe ser la misma clave utilizada
                            // por JwtService para firmar el token.
                            //
                            // Encoding.UTF8.GetBytes()
                            // convierte el texto de la clave
                            // en bytes.
                            //
                            // SymmetricSecurityKey utiliza esos bytes
                            // como clave criptográfica.
                            IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        jwtSettings["Key"]!)),


                            // Elimina el margen de tolerancia de tiempo
                            // que normalmente se aplica al validar
                            // la expiración del token.
                            //
                            // TimeSpan.Zero significa:
                            //
                            // "No permitir segundos adicionales
                            //  después de la expiración".
                            ClockSkew = TimeSpan.Zero
                        };
                });


            // Devuelve la colección de servicios para que
            // puedan continuar registrándose otras dependencias.
            return services;
        }
    }
}
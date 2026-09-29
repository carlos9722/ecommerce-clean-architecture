using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Domain.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    /// <summary>
    /// Centraliza el registro de los servicios de la capa Domain
    /// en el contenedor de inyección de dependencias de .NET.
    ///
    /// Aquí se define qué implementación concreta debe utilizar .NET
    /// cuando una clase solicite una determinada interfaz del Domain.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Registra las dependencias necesarias para la capa Domain.
        ///
        /// El parámetro 'this' convierte este método en un método de extensión
        /// de IServiceCollection, permitiendo utilizarlo directamente desde
        /// builder.Services:
        ///
        /// builder.Services.AddDomainServices();
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios de .NET donde se registran
        /// las dependencias que podrán ser creadas e inyectadas
        /// automáticamente.
        /// </param>
        /// <returns>
        /// La misma colección de servicios después de registrar
        /// las dependencias del Domain.
        /// </returns>
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services)
        {
            /*
             * ============================================================
             * CUSTOMERS DOMAIN
             * ============================================================
             *
             * Registra la relación:
             *
             * ICustomersDomain → CustomersDomain
             *
             * Cuando una clase solicite ICustomersDomain mediante
             * inyección de dependencias, .NET proporcionará
             * automáticamente una instancia de CustomersDomain.
             *
             * AddScoped indica que normalmente se utilizará una
             * instancia durante cada petición HTTP.
             */
            services.AddScoped<
                ICustomersDomain,
                CustomersDomain>();


            /*
             * ============================================================
             * USERS DOMAIN
             * ============================================================
             *
             * Registra la relación:
             *
             * IUsersDomain → UsersDomain
             *
             * Cuando una clase solicite IUsersDomain mediante
             * inyección de dependencias, .NET proporcionará
             * automáticamente una instancia de UsersDomain.
             *
             * Este servicio será utilizado, por ejemplo, por
             * AuthApplication para realizar operaciones relacionadas
             * con los usuarios:
             *
             * - Buscar un usuario por email.
             * - Crear un usuario.
             * - Verificar una contraseña.
             */
            services.AddScoped<
                IUsersDomain,
                UsersDomain>();


            /*
             * Devuelve el contenedor de servicios.
             *
             * Esto permite continuar registrando otras dependencias
             * después de llamar a AddDomainServices().
             */
            return services;
        }
    }
}
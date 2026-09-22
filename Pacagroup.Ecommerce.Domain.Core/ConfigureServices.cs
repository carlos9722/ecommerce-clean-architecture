using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Domain.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    /// <summary>
    /// Centraliza el registro de los servicios de la capa de dominio
    /// en el contenedor de inyección de dependencias de .NET.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Registra las dependencias necesarias para la capa de dominio.
        /// 
        /// El parámetro 'this' convierte este método en un método de extensión
        /// de IServiceCollection, permitiendo utilizarlo directamente desde
        /// builder.Services, por ejemplo: builder.Services.AddDomainServices().
        /// </summary>
        /// <param name="services">
        /// Contenedor donde se registran las dependencias que .NET
        /// podrá crear e inyectar automáticamente.
        /// </param>
        /// <returns>
        /// La misma colección de servicios después de registrar
        /// las dependencias del dominio.
        /// </returns>
        public static IServiceCollection AddDomainServices(
            this IServiceCollection services)
        {
            /*
             * Indica a .NET cómo resolver ICustomersDomain:
             *
             * ICustomersDomain → CustomersDomain
             *
             * Es decir, cuando una clase solicite ICustomersDomain
             * mediante inyección de dependencias, .NET proporcionará
             * una instancia de CustomersDomain.
             *
             * AddScoped indica que la instancia tendrá una duración
             * Scoped: en una aplicación web, normalmente una instancia
             * durante cada petición HTTP.
             */
            services.AddScoped<ICustomersDomain, CustomersDomain>();

            // Devuelve el contenedor para poder continuar configurándolo.
            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    /// <summary>
    /// Centraliza el registro de las dependencias de la capa Infrastructure
    /// en el contenedor de inyección de dependencias de .NET.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Registra los componentes de Infrastructure que podrán ser
        /// inyectados automáticamente por .NET.
        /// 
        /// Al ser un método de extensión, puede utilizarse directamente
        /// sobre IServiceCollection:
        /// builder.Services.AddInfrastructureServices();
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios donde se registrarán las dependencias.
        /// </param>
        /// <returns>
        /// La misma colección de servicios después de registrar
        /// las dependencias de Infrastructure.
        /// </returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // DapperContext se registra como Singleton:
            // se utiliza una única instancia de DapperContext durante
            // toda la vida de la aplicación.
            //
            // Importante: esto NO significa que exista una única conexión
            // a SQL Server. CreateConnection() crea una nueva SqlConnection
            // cada vez que se invoca.
            services.AddSingleton<DapperContext>();

            // Cuando alguna clase solicite ICustomersRepository,
            // .NET proporcionará una instancia de CustomersRepository.
            //
            // Scoped: normalmente una instancia por petición HTTP.
            services.AddScoped<ICustomersRepository, CustomersRepository>();

            // Cuando alguna clase solicite IUnitOfWork,
            // .NET proporcionará una instancia de UnitOfWork.
            //
            // Scoped: normalmente una instancia por petición HTTP.
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Devuelve el contenedor para continuar registrando servicios.
            return services;
        }
    }
}
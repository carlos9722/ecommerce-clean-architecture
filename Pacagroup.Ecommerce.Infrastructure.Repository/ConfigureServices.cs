using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    /// <summary>
    /// Centraliza el registro de las dependencias de la capa Infrastructure
    /// en el contenedor de inyección de dependencias de .NET.
    ///
    /// Aquí se registran componentes relacionados con:
    /// - Acceso a la base de datos.
    /// - Repositorios.
    /// - Unit of Work.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Registra los componentes de Infrastructure que podrán ser
        /// creados e inyectados automáticamente por .NET.
        ///
        /// Al ser un método de extensión, puede utilizarse directamente
        /// sobre IServiceCollection:
        ///
        /// builder.Services.AddInfrastructureServices();
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios de .NET donde se registrarán
        /// las dependencias de Infrastructure.
        /// </param>
        /// <returns>
        /// La misma colección de servicios después de registrar
        /// las dependencias de Infrastructure.
        /// </returns>
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services)
        {
            /*
             * ============================================================
             * DAPPER CONTEXT
             * ============================================================
             *
             * Registra DapperContext como Singleton.
             *
             * Singleton significa que .NET utilizará una única instancia
             * de DapperContext durante toda la vida de la aplicación.
             *
             * IMPORTANTE:
             * Singleton NO significa una única conexión a SQL Server.
             *
             * DapperContext crea una nueva conexión cada vez que
             * se ejecuta:
             *
             * _context.CreateConnection();
             */
            services.AddSingleton<DapperContext>();


            /*
             * ============================================================
             * CUSTOMERS REPOSITORY
             * ============================================================
             *
             * Registra la relación:
             *
             * ICustomersRepository → CustomersRepository
             *
             * Cuando una clase solicite ICustomersRepository,
             * .NET proporcionará automáticamente una instancia
             * de CustomersRepository.
             *
             * AddScoped significa que normalmente se utilizará
             * una instancia durante cada petición HTTP.
             */
            services.AddScoped<
                ICustomersRepository,
                CustomersRepository>();


            /*
             * ============================================================
             * USERS REPOSITORY
             * ============================================================
             *
             * Registra la relación:
             *
             * IUsersRepository → UsersRepository
             *
             * Cuando una clase solicite IUsersRepository,
             * .NET proporcionará automáticamente una instancia
             * de UsersRepository.
             *
             * Este repositorio se encargará de las operaciones
             * relacionadas con los usuarios y el acceso a datos.
             */
            services.AddScoped<
                IUsersRepository,
                UsersRepository>();


            /*
             * ============================================================
             * UNIT OF WORK
             * ============================================================
             *
             * Registra la relación:
             *
             * IUnitOfWork → UnitOfWork
             *
             * Cuando una clase solicite IUnitOfWork,
             * .NET proporcionará automáticamente una instancia
             * de UnitOfWork.
             *
             * UnitOfWork permite acceder a los diferentes repositorios,
             * por ejemplo:
             *
             * _unitOfWork.Customers
             * _unitOfWork.Users
             *
             * También se registra como Scoped, por lo que normalmente
             * tendrá una instancia por petición HTTP.
             */
            services.AddScoped<
                IUnitOfWork,
                UnitOfWork>();


            /*
             * Devuelve el contenedor de servicios.
             *
             * Esto permite continuar configurando otras dependencias
             * después de llamar a AddInfrastructureServices().
             */
            return services;
        }
    }
}
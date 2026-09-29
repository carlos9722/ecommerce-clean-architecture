using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using System.Reflection;

namespace Pacagroup.Ecommerce.Application.Main
{
    /// <summary>
    /// Centraliza el registro de las dependencias de la capa Application
    /// en el contenedor de inyección de dependencias de .NET.
    ///
    /// Aquí se indica a .NET qué implementación concreta debe utilizar
    /// cuando alguna clase solicite una determinada interfaz.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Registra los servicios necesarios para que la capa Application
        /// pueda ser utilizada mediante inyección de dependencias.
        ///
        /// También registra AutoMapper y busca automáticamente los perfiles
        /// de mapeo definidos dentro del ensamblado de Application.
        /// </summary>
        /// <param name="services">
        /// Contenedor de servicios de .NET donde se registrarán
        /// las dependencias de Application.
        /// </param>
        /// <returns>
        /// La misma colección de servicios después de registrar
        /// las dependencias de Application.
        /// </returns>
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services)
        {
            /*
             * ============================================================
             * CUSTOMERS APPLICATION
             * ============================================================
             *
             * Registra la relación:
             *
             * ICustomersApplication → CustomersApplication
             *
             * Cuando una clase solicite ICustomersApplication mediante
             * inyección de dependencias, .NET proporcionará una instancia
             * de CustomersApplication.
             *
             * AddScoped significa que normalmente se utilizará una
             * instancia durante cada petición HTTP.
             */
            services.AddScoped<
                ICustomersApplication,
                CustomersApplication>();


            /*
             * ============================================================
             * AUTH APPLICATION
             * ============================================================
             *
             * Registra la relación:
             *
             * IAuthApplication → AuthApplication
             *
             * Permite que un Controller u otra clase pueda solicitar
             * IAuthApplication y .NET proporcione automáticamente
             * una instancia de AuthApplication.
             *
             * AuthApplication se encarga de coordinar operaciones
             * como:
             *
             * - Registro de usuarios.
             * - Inicio de sesión.
             * - Generación del token de autenticación.
             */
            services.AddScoped<
                IAuthApplication,
                AuthApplication>();


            /*
             * ============================================================
             * JWT SERVICE
             * ============================================================
             *
             * Registra la relación:
             *
             * IJwtService → JwtService
             *
             * Cuando AuthApplication solicite IJwtService
             * en su constructor, .NET proporcionará automáticamente
             * una instancia de JwtService.
             *
             * Esto permite que AuthApplication dependa de la interfaz
             * y no directamente de la implementación concreta.
             */
            services.AddScoped<
                IJwtService,
                JwtService>();


            /*
             * ============================================================
             * AUTOMAPPER
             * ============================================================
             *
             * Registra AutoMapper dentro del contenedor de DI.
             *
             * Assembly.GetExecutingAssembly() obtiene el ensamblado
             * (proyecto compilado) donde se está ejecutando este código.
             *
             * AutoMapper utiliza ese ensamblado para buscar clases
             * que hereden de Profile, por ejemplo:
             *
             * MappingsProfile
             *
             * Gracias a esto podemos utilizar IMapper mediante
             * inyección de dependencias:
             *
             * _mapper.Map<Customer>(customerDto);
             *
             * o:
             *
             * _mapper.Map<CustomerDto>(customer);
             *
             * sin crear manualmente una instancia de IMapper.
             */
            services.AddAutoMapper(
                cfg => { },
                Assembly.GetExecutingAssembly());


            /*
             * Devuelve el contenedor de servicios.
             *
             * Esto permite continuar registrando otros servicios
             * después de llamar a AddApplicationServices().
             */
            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Interface;
using System.Reflection;

namespace Pacagroup.Ecommerce.Application.Main
{
    /// <summary>
    /// Centraliza el registro de las dependencias de la capa Application
    /// en el contenedor de inyección de dependencias de .NET.
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
             * Registra la implementación de ICustomersApplication.
             *
             * Cuando alguna clase solicite ICustomersApplication
             * mediante inyección de dependencias, .NET proporcionará
             * una instancia de CustomersApplication.
             *
             * AddScoped → normalmente se crea una instancia por cada
             * petición HTTP.
             *
             * Relación:
             *
             * ICustomersApplication → CustomersApplication
             */
            services.AddScoped<ICustomersApplication, CustomersApplication>();

            /*
             * Registra AutoMapper en el contenedor de DI.
             *
             * Assembly.GetExecutingAssembly() obtiene el ensamblado
             * (proyecto/compilado) donde se está ejecutando este código.
             *
             * AutoMapper utiliza ese ensamblado para buscar los perfiles
             * de mapeo, como MappingsProfile.
             *
             * Gracias a esto, posteriormente podemos utilizar:
             *
             * _mapper.Map<Customer>(customersDto);
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

            // Devuelve el contenedor para poder continuar
            // registrando otros servicios.
            return services;
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Pacagroup.Ecommerce.Transversal.Logging
{
    /// <summary>
    /// Contiene la configuración de los servicios transversales
    /// relacionados con logging.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Configura Serilog y registra los servicios de logging
        /// personalizados en el contenedor de dependencias.
        /// </summary>
        /// <param name="services">
        /// Colección de servicios donde se registran las dependencias.
        /// </param>
        /// <param name="configuration">
        /// Configuración de la aplicación utilizada para obtener
        /// valores como la cadena de conexión.
        /// </param>
        /// <returns>
        /// La colección de servicios con las dependencias registradas.
        /// </returns>
        public static IServiceCollection AddTransversalServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Configura el logger global de Serilog.
            //
            // LoggerConfiguration permite definir:
            // - Qué niveles de log se registran.
            // - Qué información adicional se agrega.
            // - Dónde se almacenan los logs.
            Log.Logger = new LoggerConfiguration()

                // Registra eventos desde Information en adelante:
                // Information, Warning, Error y Fatal.
                .MinimumLevel.Information()

                // Los logs generados por Microsoft solo se registran
                // cuando tienen nivel Warning o superior.
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)

                // Los logs generados por System solo se registran
                // cuando tienen nivel Warning o superior.
                .MinimumLevel.Override("System", LogEventLevel.Warning)

                // Permite agregar información relacionada con el
                // contexto de la solicitud actual.
                .Enrich.FromLogContext()

                // Agrega una propiedad adicional llamada Application
                // a los registros de log.
                .Enrich.WithProperty("Application", "Pacagroup.Ecommerce")

                // Envía los logs a la consola.
                //
                // outputTemplate define el formato de cada registro.
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")

                // Guarda los logs en archivos de texto.
                .WriteTo.File(
                    // Ubicación y nombre base de los archivos.
                    path: "logs/log-.txt",

                    // Crea un archivo nuevo cada día.
                    rollingInterval: RollingInterval.Day,

                    // Conserva como máximo 30 archivos.
                    retainedFileCountLimit: 30,

                    // Formato utilizado para escribir los registros.
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}")

                // Envía los logs a una tabla de SQL Server.
                .WriteTo.MSSqlServer(

                    // Obtiene la cadena de conexión llamada
                    // "NorthwindConnection" desde la configuración.
                    connectionString: configuration.GetConnectionString("NorthwindConnection"),

                    // Configura el destino SQL Server.
                    sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        // Nombre de la tabla donde se almacenarán
                        // los registros.
                        TableName = "Logs",

                        // Indica que Serilog puede crear automáticamente
                        // la tabla si todavía no existe.
                        AutoCreateSqlTable = true
                    },

                    // Solo envía a SQL Server logs con nivel
                    // Warning, Error o Fatal.
                    //
                    // Los logs Information no se almacenan
                    // en SQL Server.
                    restrictedToMinimumLevel: LogEventLevel.Warning)

                // Construye el logger con toda la configuración anterior.
                .CreateLogger();


            // Registra Serilog dentro del sistema de servicios
            // de ASP.NET Core.
            services.AddSerilog();

            // Register custom logger
            //
            // Cuando una clase solicite IAppLogger<T>,
            // el sistema de DI proporcionará AppLogger<T>.
            //
            // AddScoped significa que se crea una instancia
            // por cada solicitud HTTP.
            services.AddScoped(typeof(IAppLogger<>), typeof(AppLogger<>));


            // Devuelve la colección de servicios para continuar
            // registrando otras dependencias.
            return services;
        }
    }
}
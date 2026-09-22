using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Pacagroup.Ecommerce.Infrastructure.Data
{
    /// <summary>
    /// Contexto encargado de crear conexiones hacia la base de datos
    /// utilizando la cadena de conexión configurada en la aplicación.
    /// </summary>
    public class DapperContext
    {
        /// <summary>
        /// Permite acceder a la configuración de la aplicación,
        /// incluyendo las cadenas de conexión definidas en appsettings.json.
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Cadena de conexión utilizada para conectarse a SQL Server.
        /// El '?' indica que puede ser null.
        /// </summary>
        private readonly string? _connectionString;

        /// <summary>
        /// Recibe la configuración mediante inyección de dependencias
        /// y obtiene la cadena de conexión llamada "NorthwindConnection".
        /// </summary>
        /// <param name="configuration">
        /// Configuración de la aplicación proporcionada por .NET.
        /// </param>
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;

            // Obtiene la cadena de conexión desde appsettings.json.
            _connectionString =
                _configuration.GetConnectionString("NorthwindConnection");
        }

        /// <summary>
        /// Crea y devuelve una conexión hacia SQL Server.
        /// </summary>
        /// <returns>
        /// Una conexión que implementa IDbConnection.
        /// </returns>
        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
    }
}
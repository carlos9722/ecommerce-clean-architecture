using Dapper;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    /// <summary>
    /// Implementa el repositorio de Customer y se encarga de realizar
    /// las operaciones de acceso a datos mediante Dapper y SQL Server.
    /// </summary>
    public class CustomersRepository : ICustomersRepository
    {
        /// <summary>
        /// Contexto utilizado para crear las conexiones hacia la base de datos.
        /// </summary>
        private readonly DapperContext _context;

        /// <summary>
        /// Constructor de la clase.
        /// Recibe DapperContext mediante inyección de dependencias y lo guarda
        /// en _context para poder utilizarlo en las operaciones del repositorio.
        /// </summary>
        /// <param name="context">
        /// Contexto encargado de crear las conexiones a SQL Server.
        /// </param>
        public CustomersRepository(DapperContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todos los clientes mediante el procedimiento almacenado
        /// CustomersList.
        /// </summary>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            // Crea una conexión a la base de datos.
            using var connection = _context.CreateConnection();

            // Nombre del procedimiento almacenado que se ejecutará.
            var query = "CustomersList";

            // Dapper ejecuta el procedimiento y convierte cada registro
            // obtenido en un objeto Customer.
            var customers = await connection.QueryAsync<Customer>(
                query,
                commandType: CommandType.StoredProcedure);

            return customers;
        }

        /// <summary>
        /// Obtiene un cliente por su identificador.
        /// </summary>
        /// <param name="customerId">Identificador del cliente.</param>
        /// <returns>
        /// El cliente encontrado o null si no existe.
        /// </returns>
        public async Task<Customer?> GetAsync(string customerId)
        {
            using var connection = _context.CreateConnection();

            // Nombre del procedimiento almacenado.
            var query = "CustomersGetByID";

            // Parámetros que se enviarán al procedimiento almacenado.
            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);


            // Busca un único registro mediante un procedimiento almacenado y lo mapea a Customer.
            // Si el registro existe lo devuelve; si no existe, devuelve null de forma segura.
            var customer = await connection.QuerySingleOrDefaultAsync<Customer>(
                query,
                param: parameters,
                commandType: CommandType.StoredProcedure);

            return customer;
        }

        /// <summary>
        /// Inserta un nuevo cliente en la base de datos.
        /// </summary>
        /// <param name="customer">Cliente que se desea insertar.</param>
        /// <returns>
        /// true si se insertó al menos un registro; de lo contrario, false.
        /// </returns>
        public async Task<bool> InsertAsync(Customer customer)
        {
            using var connection = _context.CreateConnection();

            var query = "CustomersInsert";

            var parameters = new DynamicParameters();

            // Se agregan los datos del cliente como parámetros
            // para el procedimiento almacenado.
            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            // ExecuteAsync devuelve la cantidad de registros afectados.
            var result = await connection.ExecuteAsync(
                query,
                param: parameters,
                commandType: CommandType.StoredProcedure);

            // Si se afectó al menos un registro, la operación fue exitosa.
            // Ejemplo: result = 1 → 1 > 0 → true.
            // Si result = 0 → 0 > 0 → false.
            return result > 0;
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="customer">Cliente con los datos actualizados.</param>
        /// <returns>
        /// true si se actualizó al menos un registro; de lo contrario, false.
        /// </returns>
        public async Task<bool> UpdateAsync(Customer customer)
        {
            using var connection = _context.CreateConnection();

            var query = "CustomersUpdate";

            var parameters = new DynamicParameters();

            parameters.Add("CustomerID", customer.CustomerId);
            parameters.Add("CompanyName", customer.CompanyName);
            parameters.Add("ContactName", customer.ContactName);
            parameters.Add("ContactTitle", customer.ContactTitle);
            parameters.Add("Address", customer.Address);
            parameters.Add("City", customer.City);
            parameters.Add("Region", customer.Region);
            parameters.Add("PostalCode", customer.PostalCode);
            parameters.Add("Country", customer.Country);
            parameters.Add("Phone", customer.Phone);
            parameters.Add("Fax", customer.Fax);

            var result = await connection.ExecuteAsync(
                query,
                param: parameters,
                commandType: CommandType.StoredProcedure);

            // Devuelve true cuando al menos un registro fue afectado.
            return result > 0;
        }

        /// <summary>
        /// Elimina un cliente por su identificador.
        /// </summary>
        /// <param name="customerId">Identificador del cliente.</param>
        /// <returns>
        /// true si se eliminó al menos un registro; de lo contrario, false.
        /// </returns>
        public async Task<bool> DeleteAsync(string customerId)
        {
            using var connection = _context.CreateConnection();

            var query = "CustomersDelete";

            var parameters = new DynamicParameters();
            parameters.Add("CustomerID", customerId);

            var result = await connection.ExecuteAsync(
                query,
                param: parameters,
                commandType: CommandType.StoredProcedure);

            // Si result > 0 significa que al menos un registro fue eliminado.
            return result > 0;
        }
    }
}
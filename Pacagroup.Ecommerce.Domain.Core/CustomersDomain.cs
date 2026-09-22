using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    /// <summary>
    /// Implementa las operaciones definidas por ICustomersDomain
    /// para gestionar los clientes del sistema.
    /// </summary>
    public class CustomersDomain : ICustomersDomain
    {
        /// <summary>
        /// Unit of Work utilizado para acceder a los repositorios
        /// de la capa de infraestructura.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor de CustomersDomain.
        /// Recibe IUnitOfWork mediante inyección de dependencias
        /// y lo guarda para utilizar los repositorios disponibles.
        /// </summary>
        /// <param name="unitOfWork">
        /// Unit of Work que proporciona acceso a los repositorios.
        /// </param>
        public CustomersDomain(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Elimina un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador único del cliente que se desea eliminar.
        /// </param>
        /// <returns>
        /// Un Task que devuelve true si la eliminación fue exitosa;
        /// de lo contrario, false.
        /// </returns>
        public async Task<bool> DeleteAsync(string customerId)
        {
            // Accede al repositorio de clientes a través del Unit of Work.
            return await _unitOfWork.Customers.DeleteAsync(customerId);
        }

        /// <summary>
        /// Obtiene todos los clientes disponibles.
        /// </summary>
        /// <returns>
        /// Un Task que devuelve una colección de clientes.
        /// </returns>
        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            // Solicita al repositorio la lista de clientes.
            return await _unitOfWork.Customers.GetAllAsync();
        }

        /// <summary>
        /// Obtiene un cliente específico mediante su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador único del cliente que se desea consultar.
        /// </param>
        /// <returns>
        /// Un Task que devuelve el cliente encontrado.
        /// </returns>
        public async Task<Customer> GetAsync(string customerId)
        {
            // Consulta el cliente utilizando el repositorio.
            return await _unitOfWork.Customers.GetAsync(customerId);
        }

        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="customer">
        /// Objeto Customer que contiene los datos del nuevo cliente.
        /// </param>
        /// <returns>
        /// Un Task que devuelve true si el registro fue exitoso;
        /// de lo contrario, false.
        /// </returns>
        public async Task<bool> InsertAsync(Customer customer)
        {
            // Envía el cliente al repositorio para realizar la inserción.
            return await _unitOfWork.Customers.InsertAsync(customer);
        }

        /// <summary>
        /// Actualiza la información de un cliente existente.
        /// </summary>
        /// <param name="customer">
        /// Objeto Customer que contiene los datos actualizados.
        /// </param>
        /// <returns>
        /// Un Task que devuelve true si la actualización fue exitosa;
        /// de lo contrario, false.
        /// </returns>
        public async Task<bool> UpdateAsync(Customer customer)
        {
            // Envía el cliente al repositorio para realizar la actualización.
            return await _unitOfWork.Customers.UpdateAsync(customer);
        }
    }
}
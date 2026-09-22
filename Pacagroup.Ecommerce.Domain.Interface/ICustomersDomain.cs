using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Domain.Interface
{
    /// <summary>
    /// Define las operaciones disponibles para gestionar clientes
    /// dentro de la capa de dominio.
    /// 
    /// La interfaz establece el "contrato" que debe cumplir cualquier
    /// implementación encargada de trabajar con clientes.
    /// </summary>
    public interface ICustomersDomain
    {
        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="customer">
        /// Cliente que contiene la información que se desea registrar.
        /// </param>
        /// <returns>
        /// Un Task que representa una operación asíncrona y devuelve
        /// true si el registro fue realizado correctamente; de lo contrario, false.
        /// </returns>
        Task<bool> InsertAsync(Customer customer);

        /// <summary>
        /// Actualiza la información de un cliente existente.
        /// </summary>
        /// <param name="customer">
        /// Cliente con la información actualizada.
        /// </param>
        /// <returns>
        /// Un Task que devuelve true si la actualización fue realizada
        /// correctamente; de lo contrario, false.
        /// </returns>
        Task<bool> UpdateAsync(Customer customer);

        /// <summary>
        /// Elimina un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador único del cliente que se desea eliminar.
        /// </param>
        /// <returns>
        /// Un Task que devuelve true si el cliente fue eliminado correctamente;
        /// de lo contrario, false.
        /// </returns>
        Task<bool> DeleteAsync(string customerId);

        /// <summary>
        /// Obtiene un cliente específico mediante su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador único del cliente que se desea consultar.
        /// </param>
        /// <returns>
        /// Un Task que contiene el cliente encontrado.
        /// </returns>
        Task<Customer> GetAsync(string customerId);

        /// <summary>
        /// Obtiene todos los clientes disponibles.
        /// </summary>
        /// <returns>
        /// Un Task que contiene una colección de clientes.
        /// IEnumerable permite recorrer los resultados sin depender
        /// de una implementación concreta de colección.
        /// </returns>
        Task<IEnumerable<Customer>> GetAllAsync();
    }
}
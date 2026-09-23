using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface
{
    /// <summary>
    /// Define las operaciones disponibles en la capa Application
    /// para gestionar los clientes.
    ///
    /// Esta interfaz establece qué operaciones puede realizar la aplicación,
    /// pero no define cómo se ejecutan.
    /// </summary>
    public interface ICustomersApplication
    {
        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="customersDto">
        /// DTO que contiene los datos del cliente que se desea registrar.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si el registro fue exitoso.
        /// </returns>
        Task<Response<bool>> InsertAsync(CustomerDto customersDto);

        /// <summary>
        /// Actualiza la información de un cliente existente.
        /// </summary>
        /// <param name="customersDto">
        /// DTO que contiene los datos actualizados del cliente.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si la actualización fue exitosa.
        /// </returns>
        Task<Response<bool>> UpdateAsync(CustomerDto customersDto);

        /// <summary>
        /// Elimina un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente que se desea eliminar.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si la eliminación fue exitosa.
        /// </returns>
        Task<Response<bool>> DeleteAsync(string customerId);

        /// <summary>
        /// Obtiene un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente que se desea consultar.
        /// </param>
        /// <returns>
        /// Response<CustomerDto> que contiene los datos del cliente.
        /// </returns>
        Task<Response<CustomerDto>> GetAsync(string customerId);

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>
        /// Response que contiene una colección de CustomerDto.
        /// </returns>
        Task<Response<IEnumerable<CustomerDto>>> GetAllAsync();
    }
}
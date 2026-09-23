using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Main
{
    /// <summary>
    /// Implementa las operaciones definidas por ICustomersApplication
    /// para gestionar los clientes desde la capa Application.
    ///
    /// Esta clase coordina el flujo entre Application y Domain:
    /// DTO → Entidad → Domain → Resultado → Response.
    /// </summary>
    public class CustomersApplication : ICustomersApplication
    {
        /// <summary>
        /// Permite acceder a las operaciones de clientes definidas
        /// en la capa Domain.
        ///
        /// Se recibe mediante inyección de dependencias.
        /// </summary>
        private readonly ICustomersDomain _customersDomain;

        /// <summary>
        /// Servicio de AutoMapper utilizado para convertir objetos
        /// entre DTOs y entidades.
        ///
        /// Ejemplo:
        /// CustomerDto → Customer
        /// Customer → CustomerDto
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor de CustomersApplication.
        ///
        /// Recibe las dependencias necesarias mediante inyección
        /// de dependencias y las guarda en sus respectivos campos.
        /// </summary>
        /// <param name="customersDomain">
        /// Servicio del Domain encargado de ejecutar las operaciones
        /// relacionadas con los clientes.
        /// </param>
        /// <param name="mapper">
        /// Servicio de AutoMapper utilizado para realizar conversiones
        /// entre DTOs y entidades.
        /// </param>
        public CustomersApplication(
            ICustomersDomain customersDomain,
            IMapper mapper)
        {
            _customersDomain = customersDomain;
            _mapper = mapper;
        }

        /// <summary>
        /// Registra un nuevo cliente.
        ///
        /// Recibe un CustomerDto, lo convierte a Customer mediante
        /// AutoMapper y envía la entidad al Domain para realizar
        /// la operación.
        /// </summary>
        /// <param name="customersDto">
        /// DTO con los datos del cliente que se desea registrar.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si el registro fue exitoso.
        /// </returns>
        public async Task<Response<bool>> InsertAsync(CustomerDto customersDto)
        {
            // Crea la estructura estándar que contendrá
            // el resultado de la operación.
            var response = new Response<bool>();

            try
            {
                // Convierte el DTO recibido desde Application/API
                // a la entidad Customer utilizada por Domain.
                var customer = _mapper.Map<Customer>(customersDto);

                // Envía la entidad al Domain para realizar el registro.
                // El resultado (true/false) se guarda en Data.
                response.Data = await _customersDomain.InsertAsync(customer);

                // Si el Domain indica que el registro fue exitoso,
                // se configura la respuesta como exitosa.
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!";
                }
            }
            catch (Exception e)
            {
                // Si ocurre una excepción, se conserva el mensaje
                // del error en la respuesta.
                response.Message = e.Message;
            }

            // Devuelve la respuesta al consumidor de Application.
            return response;
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="customersDto">
        /// DTO con los datos actualizados del cliente.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si la actualización fue exitosa.
        /// </returns>
        public async Task<Response<bool>> UpdateAsync(CustomerDto customersDto)
        {
            var response = new Response<bool>();

            try
            {
                // Convierte CustomerDto a la entidad Customer.
                var customer = _mapper.Map<Customer>(customersDto);

                // Solicita al Domain realizar la actualización.
                response.Data = await _customersDomain.UpdateAsync(customer);

                // Si la operación fue exitosa, actualiza la respuesta.
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                // Guarda el mensaje de la excepción en la respuesta.
                response.Message = e.Message;
            }

            return response;
        }

        /// <summary>
        /// Elimina un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente que se desea eliminar.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si la eliminación fue exitosa.
        /// </returns>
        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();

            try
            {
                // Solicita al Domain eliminar el cliente.
                response.Data = await _customersDomain.DeleteAsync(customerId);

                // Si la operación fue exitosa, configura la respuesta.
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        /// <summary>
        /// Obtiene un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente que se desea consultar.
        /// </param>
        /// <returns>
        /// Response<CustomerDto> con los datos del cliente encontrado.
        /// </returns>
        public async Task<Response<CustomerDto>> GetAsync(string customerId)
        {
            var response = new Response<CustomerDto>();

            try
            {
                // Solicita al Domain el cliente utilizando su ID.
                var customer = await _customersDomain.GetAsync(customerId);

                // Convierte la entidad Customer obtenida desde Domain
                // nuevamente a CustomerDto para devolverla hacia Application/API.
                response.Data = _mapper.Map<CustomerDto>(customer);

                // Si existe información del cliente, la consulta
                // se considera exitosa.
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>
        /// Response que contiene una colección de CustomerDto.
        /// </returns>
        public async Task<Response<IEnumerable<CustomerDto>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<CustomerDto>>();

            try
            {
                // Solicita al Domain todos los clientes.
                var customers = await _customersDomain.GetAllAsync();

                // Convierte la colección de Customer a una colección
                // de CustomerDto mediante AutoMapper.
                response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);

                // Si existe información, la consulta se considera exitosa.
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
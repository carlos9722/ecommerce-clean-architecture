using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Pacagroup.Ecommerce.Services.WebApi.Controllers
{
    /// <summary>
    /// Controller encargado de recibir las solicitudes HTTP relacionadas
    /// con la gestión de clientes.
    ///
    /// Actúa como punto de entrada de la API y delega las operaciones
    /// a la capa Application.
    /// [Authorize] indica que todos los endpoints de este Controller
    /// requieren un usuario autenticado.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones relacionadas con Clientes")]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Servicio de Application encargado de ejecutar
        /// las operaciones relacionadas con los clientes.
        /// </summary>
        private readonly ICustomersApplication _customersApplication;

        /// <summary>
        /// Constructor del Controller.
        ///
        /// Recibe ICustomersApplication mediante inyección
        /// de dependencias y la guarda para utilizarla
        /// en los diferentes endpoints.
        /// </summary>
        /// <param name="customersApplication">
        /// Servicio de Application para gestionar clientes.
        /// </param>
        public CustomersController(
            ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }

        /// <summary>
        /// Registra un nuevo cliente.
        /// </summary>
        /// <param name="customerDto">
        /// Datos del cliente enviados en el cuerpo de la petición HTTP.
        /// </param>
        /// <returns>
        /// 200 OK si el registro fue exitoso.
        /// 400 BadRequest si los datos son inválidos.
        /// 500 InternalServerError si ocurre un error durante la operación.
        /// </returns>
        [HttpPost("InsertAsync")]
        [SwaggerOperation(
        Summary = "Registra un Cliente",
        Description = "Retorna un objeto generico con el resultado de la operación")]
        [SwaggerResponse(200, "Cliente registrado", typeof(Response<bool>))]
        public async Task<IActionResult> InsertAsync(
            [FromBody] CustomerDto customerDto)
        {
            // Verifica que el cuerpo de la petición no sea null.
            if (customerDto == null)
                return BadRequest();

            // Envía el DTO a la capa Application para realizar
            // el registro del cliente.
            var response =
                await _customersApplication.InsertAsync(customerDto);

            // Si Application indica que la operación fue exitosa,
            // devuelve HTTP 200 (OK) junto con la respuesta.
            if (response.IsSuccess)
                return Ok(response);

            // Si la operación no fue exitosa, devuelve HTTP 500.
            return StatusCode(
                (int)HttpStatusCode.InternalServerError,
                response);
        }

        /// <summary>
        /// Actualiza un cliente existente.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente recibido desde la URL.
        /// </param>
        /// <param name="customerDto">
        /// Datos actualizados enviados en el cuerpo de la petición.
        /// </param>
        /// <returns>
        /// Resultado HTTP de la operación.
        /// </returns>
        [HttpPut("UpdateAsync/{customerId}")]
        [SwaggerOperation(
        Summary = "Actualiza un Cliente en función a su ID",
        Description = "Retorna un objeto generico con el resultado de la operación")]
        [SwaggerResponse(200, "Cliente actualizado", typeof(Response<bool>))]
        public async Task<IActionResult> UpdateAsync(
            [FromRoute] string customerId,
            [FromBody] CustomerDto customerDto)
        {
            // Verifica que el DTO recibido no sea null.
            if (customerDto == null)
                return BadRequest();

            // Verifica que el ID recibido en la URL coincida
            // con el ID enviado dentro del DTO.
            if (!customerId.Equals(customerDto.CustomerId))
                return BadRequest();

            // Envía el DTO a Application para realizar
            // la actualización.
            var response =
                await _customersApplication.UpdateAsync(customerDto);

            // Devuelve HTTP 200 si la operación fue exitosa.
            if (response.IsSuccess)
                return Ok(response);

            // Devuelve HTTP 500 si ocurrió un error.
            return StatusCode(
                (int)HttpStatusCode.InternalServerError,
                response);
        }

        /// <summary>
        /// Elimina un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente recibido desde la URL.
        /// </param>
        /// <returns>
        /// Resultado HTTP de la operación.
        /// </returns>
        [HttpDelete("DeleteAsync/{customerId}")]
        [SwaggerOperation(
        Summary = "Elimina un Cliente en función a su ID",
        Description = "Retorna un objeto generico con el resultado de la operación")]
        [SwaggerResponse(200, "Cliente eliminado", typeof(Response<bool>))]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] string customerId)
        {
            // Verifica que se haya recibido un identificador.
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            // Solicita a Application la eliminación del cliente.
            var response =
                await _customersApplication.DeleteAsync(customerId);

            // Devuelve HTTP 200 si la eliminación fue exitosa.
            if (response.IsSuccess)
                return Ok(response);

            // Devuelve HTTP 500 si ocurrió un error.
            return StatusCode(
                (int)HttpStatusCode.InternalServerError,
                response);
        }

        /// <summary>
        /// Obtiene un cliente utilizando su identificador.
        /// </summary>
        /// <param name="customerId">
        /// Identificador del cliente recibido desde la URL.
        /// </param>
        /// <returns>
        /// Resultado HTTP que contiene el cliente encontrado.
        /// </returns>
        [HttpGet("GetAsync/{customerId}")]
        [SwaggerOperation(
        Summary = "Obtiene un Cliente en función a su ID",
        Description = "Retorna un objeto generico con el resultado de la operación")]
        [SwaggerResponse(200, "Cliente encontrado", typeof(Response<CustomerDto>))]
        public async Task<IActionResult> GetAsync(
            [FromRoute] string customerId)
        {
            // Verifica que se haya recibido un identificador.
            if (string.IsNullOrEmpty(customerId))
                return BadRequest();

            // Solicita a Application la información del cliente.
            var response =
                await _customersApplication.GetAsync(customerId);

            // Devuelve HTTP 200 si el cliente fue encontrado.
            if (response.IsSuccess)
                return Ok(response);

            // Devuelve HTTP 500 si ocurrió un error.
            return StatusCode(
                (int)HttpStatusCode.InternalServerError,
                response);
        }

        /// <summary>
        /// Obtiene todos los clientes registrados.
        /// </summary>
        /// <returns>
        /// Resultado HTTP que contiene la colección de clientes.
        /// </returns>
        [HttpGet("GetAllAsync")]
        [SwaggerOperation(
        Summary = "Lista la totalidad de Clientes",
        Description = "Retorna un objeto generico con el resultado de la operación")]
        [SwaggerResponse(200, "Clientes encontrados", typeof(Response<IEnumerable<CustomerDto>>))]
        public async Task<IActionResult> GetAllAsync()
        {
            // Solicita a Application todos los clientes.
            var response =
                await _customersApplication.GetAllAsync();

            // Devuelve HTTP 200 si la consulta fue exitosa.
            if (response.IsSuccess)
                return Ok(response);

            // Devuelve HTTP 500 si ocurrió un error.
            return StatusCode(
                (int)HttpStatusCode.InternalServerError,
                response);
        }
    }
}
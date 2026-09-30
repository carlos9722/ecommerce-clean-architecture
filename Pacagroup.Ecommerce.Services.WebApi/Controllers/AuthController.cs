using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;


namespace Pacagroup.Ecommerce.Services.WebApi.Controllers
{
    /// <summary>
    /// Controller encargado de exponer los endpoints HTTP
    /// relacionados con la autenticación de usuarios.
    ///
    /// Recibe las peticiones del cliente y delega la lógica
    /// a IAuthApplication.
    /// [Authorize] requiere token
    /// Sin embargo, SignUp y SignIn tienen [AllowAnonymous],
    /// por lo que esos dos endpoints son excepciones.
    /// El Controller no accede directamente a la base de datos.
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Operaciones de Autenticación")]
    public class AuthController : ControllerBase
    {
        /*
         * Dependencia de la capa Application encargada
         * de ejecutar las operaciones de autenticación.
         *
         * Se utiliza la interfaz IAuthApplication
         * en lugar de crear directamente AuthApplication.
         */
        private readonly IAuthApplication _authApplication;


        /// <summary>
        /// Constructor del Controller.
        ///
        /// IAuthApplication se recibe mediante inyección
        /// de dependencias.
        /// </summary>
        /// <param name="authApplication">
        /// Servicio de Application encargado de realizar
        /// el registro y autenticación de usuarios.
        /// </param>
        public AuthController(IAuthApplication authApplication)
        {
            // Guardamos la dependencia para utilizarla
            // en los endpoints del Controller.
            _authApplication = authApplication;
        }


        /// <summary>
        /// Registra un nuevo usuario.
        /// </summary>
        /// <param name="signUpDto">
        /// Datos enviados por el cliente para crear la cuenta.
        /// </param>
        /// <returns>
        /// 200 OK si el registro fue exitoso.
        /// 400 BadRequest si ocurrió un problema con el registro.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("SignUp")]
        [SwaggerOperation(Summary = "Registra un nuevo usuario")]
        public async Task<IActionResult> SignUpAsync(
            [FromBody] SignUpDto signUpDto)
        {
            /*
             * [FromBody] indica que signUpDto será construido
             * utilizando el JSON enviado en el cuerpo de la petición HTTP.
             *
             * Ejemplo:
             *
             * POST /api/Auth/SignUp
             *
             * {
             *     "firstName": "Carlos",
             *     "lastName": "Perez",
             *     "email": "carlos@email.com",
             *     "userName": "carlos",
             *     "password": "123456"
             * }
             */

            /*
             * Delegamos la operación a Application.
             *
             * El Controller NO crea el usuario directamente.
             * AuthApplication se encargará de coordinar el proceso.
             */
            var response = await _authApplication.SignUpAsync(signUpDto);


            /*
             * Si Application indica que la operación fue exitosa,
             * devolvemos HTTP 200 OK junto con la respuesta.
             */
            if (response.IsSuccess)
                return Ok(response);


            /*
             * Si la operación no fue exitosa, devolvemos HTTP 400.
             *
             * BadRequest normalmente representa que la solicitud
             * no pudo procesarse correctamente.
             */
            return BadRequest(response);
        }


        /// <summary>
        /// Autentica un usuario y genera un token de acceso.
        /// </summary>
        /// <param name="signInDto">
        /// Credenciales enviadas por el cliente.
        /// </param>
        /// <returns>
        /// 200 OK si las credenciales son válidas.
        /// 401 Unauthorized si la autenticación falla.
        /// </returns>
        [AllowAnonymous]
        [HttpPost("SignIn")]
        [SwaggerOperation(Summary = "Autentica un usuario y genera token")]
        public async Task<IActionResult> SignInAsync(
            [FromBody] SignInDto signInDto)
        {
            /*
             * [FromBody] indica que las credenciales
             * se obtienen del cuerpo de la petición HTTP.
             *
             * Ejemplo:
             *
             * POST /api/Auth/SignIn
             *
             * {
             *     "email": "carlos@email.com",
             *     "password": "123456"
             * }
             */

            /*
             * Delegamos la autenticación a Application.
             *
             * AuthApplication se encargará de:
             *
             * 1. Buscar el usuario.
             * 2. Verificar la contraseña.
             * 3. Generar el JWT si las credenciales son válidas.
             */
            var response = await _authApplication.SignInAsync(signInDto);


            /*
             * Si la autenticación fue exitosa,
             * devolvemos HTTP 200 OK.
             *
             * La respuesta contiene normalmente el TokenDto.
             */
            if (response.IsSuccess)
                return Ok(response);


            /*
             * Si las credenciales no son válidas,
             * devolvemos HTTP 401 Unauthorized.
             *
             * 401 significa que la solicitud requiere
             * autenticación válida o que las credenciales
             * proporcionadas no son válidas.
             */
            return Unauthorized(response);
        }
    }
}
using AutoMapper;
using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Logging;

namespace Pacagroup.Ecommerce.Application.Main
{
    /// <summary>
    /// Implementa las operaciones de autenticación definidas
    /// por IAuthApplication.
    ///
    /// Esta clase coordina el proceso de registro e inicio de sesión.
    /// No accede directamente a la base de datos.
    /// Para ello utiliza IUsersDomain.
    /// </summary>
    public class AuthApplication : IAuthApplication
    {
        /*
         * Servicio del Domain encargado de realizar las operaciones
         * relacionadas con los usuarios.
         *
         * A través de esta dependencia podemos:
         *
         * - Buscar usuarios.
         * - Crear usuarios.
         * - Verificar contraseñas.
         *
         * La capa Application no necesita conocer cómo se realizan
         * internamente estas operaciones.
         */
        private readonly IUsersDomain _usersDomain;


        /*
         * Servicio encargado de generar los tokens de autenticación,
         * normalmente JWT.
         *
         * AuthApplication solicita el token, pero no necesita conocer
         * cómo se construye internamente.
         */
        private readonly IJwtService _jwtService;


        /*
         * Servicio de AutoMapper utilizado para convertir objetos
         * entre DTOs y entidades.
         *
         * Ejemplo:
         *
         * SignUpDto → User
         */
        private readonly IMapper _mapper;


        /*
         * Servicio de logging utilizado para registrar información
         * relacionada con errores y eventos de esta clase.
         *
         * IAppLogger<AuthApplication> indica que el logger está
         * asociado específicamente con AuthApplication.
         */
        private readonly IAppLogger<AuthApplication> _logger;


        /// <summary>
        /// Constructor de la clase.
        ///
        /// Recibe las dependencias necesarias mediante
        /// inyección de dependencias (Dependency Injection).
        /// </summary>
        /// <param name="usersDomain">
        /// Servicio del Domain encargado de las operaciones de usuarios.
        /// </param>
        /// <param name="jwtService">
        /// Servicio encargado de generar tokens de autenticación.
        /// </param>
        /// <param name="mapper">
        /// Servicio de AutoMapper para convertir DTOs y entidades.
        /// </param>
        /// <param name="logger">
        /// Servicio utilizado para registrar errores y eventos
        /// relacionados con AuthApplication.
        /// </param>
        public AuthApplication(
            IUsersDomain usersDomain,
            IJwtService jwtService,
            IMapper mapper,
            IAppLogger<AuthApplication> logger)
        {
            _usersDomain = usersDomain;
            _jwtService = jwtService;
            _mapper = mapper;
            _logger = logger;
        }


        /// <summary>
        /// Autentica a un usuario utilizando su correo electrónico
        /// y contraseña.
        ///
        /// Si las credenciales son correctas, genera un token
        /// de acceso y lo devuelve dentro de TokenDto.
        /// </summary>
        /// <param name="signInDto">
        /// DTO que contiene el email y la contraseña proporcionados
        /// por el usuario.
        /// </param>
        /// <returns>
        /// Response<TokenDto> con el token generado cuando
        /// la autenticación es exitosa.
        /// </returns>
        public async Task<Response<TokenDto>> SignInAsync(
            SignInDto signInDto)
        {
            // Creamos la respuesta estándar de la aplicación.
            //
            // Inicialmente:
            // Data      → valor por defecto.
            // IsSuccess → false.
            // Message   → string.Empty.
            var response = new Response<TokenDto>();

            try
            {
                /*
                 * Buscamos al usuario mediante su correo electrónico.
                 *
                 * Application → IUsersDomain
                 *
                 * La Application no consulta directamente la base de datos.
                 */
                var user = await _usersDomain.GetByEmailAsync(
                    signInDto.Email);


                // Si no encontramos el usuario, terminamos la operación.
                if (user == null)
                {
                    response.Message =
                        "Email no existe o no se encuentra registrado";

                    // Registramos en el log el motivo por el que
                    // no fue posible validar el correo electrónico.
                    _logger.LogError("Failed to validate email. Error: {Message}", response.Message);

                    return response;
                }


                /*
                 * Verificamos si la contraseña proporcionada
                 * coincide con el hash almacenado del usuario.
                 *
                 * signInDto.Password
                 *        ↓
                 * contraseña ingresada
                 *
                 * user
                 *        ↓
                 * usuario que contiene el PasswordHash
                 */
                var isValidPassword =
                    await _usersDomain.CheckPasswordAsync(
                        user,
                        signInDto.Password);


                // Si la contraseña no es válida, no generamos el token.
                if (!isValidPassword)
                {
                    response.Message = "Credenciales inválidas";

                    return response;
                }


                /*
                 * Las credenciales son correctas.
                 *
                 * Solicitamos al servicio JWT que genere
                 * un token para el usuario autenticado.
                 */
                var token = _jwtService.GenerateToken(user);


                /*
                 * Construimos el DTO que será devuelto al cliente.
                 *
                 * AccessToken → token generado.
                 * ExpiresIn   → duración del token en segundos.
                 *
                 * 3600 segundos = 1 hora.
                 */
                response.Data = new TokenDto
                {
                    AccessToken = token,
                    ExpiresIn = 3600
                };


                // Indicamos que la autenticación fue exitosa.
                response.IsSuccess = true;
                response.Message = "Autenticación exitosa";
            }
            catch (Exception e)
            {
                /*
                 * Si ocurre una excepción durante el proceso,
                 * guardamos el mensaje en la respuesta.
                 */
                response.Message = e.Message;
            }


            // Devolvemos la respuesta final.
            return response;
        }


        /// <summary>
        /// Registra un nuevo usuario en la aplicación.
        ///
        /// Primero verifica si ya existe un usuario con el mismo email.
        /// Si no existe, convierte el DTO en una entidad User y solicita
        /// al Domain la creación del usuario.
        /// </summary>
        /// <param name="signUpDto">
        /// DTO que contiene los datos necesarios para registrar
        /// al usuario.
        /// </param>
        /// <returns>
        /// Response<bool> indicando si el usuario fue creado
        /// correctamente.
        /// </returns>
        public async Task<Response<bool>> SignUpAsync(
            SignUpDto signUpDto)
        {
            // Creamos la respuesta estándar.
            var response = new Response<bool>();

            try
            {
                /*
                 * Antes de crear el usuario comprobamos si ya existe
                 * otro usuario con el mismo correo electrónico.
                 */
                var existingUser =
                    await _usersDomain.GetByEmailAsync(
                        signUpDto.Email);


                // Si encontramos un usuario, no permitimos duplicarlo.
                if (existingUser != null)
                {
                    response.Message = "El usuario ya existe";

                    return response;
                }


                /*
                 * Convertimos el DTO recibido por la API
                 * en una entidad del Domain.
                 *
                 * SignUpDto → User
                 *
                 * AutoMapper utiliza la configuración definida
                 * en MappingsProfile.
                 */
                var user = _mapper.Map<User>(signUpDto);


                /*
                 * Solicitamos al Domain que cree el usuario.
                 *
                 * También enviamos la contraseña original para que
                 * el proceso de creación pueda generar el hash
                 * correspondiente antes de almacenarla.
                 */
                response.Data =
                    await _usersDomain.CreateUserAsync(
                        user,
                        signUpDto.Password);


                /*
                 * Si la creación devuelve true, consideramos
                 * que el registro fue exitoso.
                 */
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Usuario creado exitosamente";
                }
            }
            catch (Exception e)
            {
                // Capturamos cualquier excepción producida durante
                // el proceso y la informamos mediante Response.
                response.Message = e.Message;
            }


            // Devolvemos la respuesta final.
            return response;
        }
    }
}
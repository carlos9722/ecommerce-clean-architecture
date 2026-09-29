using Pacagroup.Ecommerce.Application.DTO;
using Pacagroup.Ecommerce.Transversal.Common;

namespace Pacagroup.Ecommerce.Application.Interface
{
    /// <summary>
    /// Define las operaciones disponibles en la capa Application
    /// relacionadas con la autenticación y gestión de acceso
    /// de los usuarios.
    ///
    /// La interfaz define QUÉ operaciones puede realizar la aplicación,
    /// pero no define CÓMO se ejecutan.
    /// </summary>
    public interface IAuthApplication
    {
        /// <summary>
        /// Registra un nuevo usuario en la aplicación.
        ///
        /// Recibe los datos de registro mediante SignUpDto
        /// y devuelve un Response<bool> indicando si el registro
        /// fue exitoso.
        /// </summary>
        /// <param name="signUpDto">
        /// DTO que contiene los datos necesarios para registrar
        /// al nuevo usuario.
        /// </param>
        /// <returns>
        /// Una operación asíncrona que devuelve un Response<bool>.
        ///
        /// Data = true  → registro exitoso.
        /// Data = false → registro no exitoso.
        /// </returns>
        Task<Response<bool>> SignUpAsync(SignUpDto signUpDto);


        /// <summary>
        /// Autentica a un usuario utilizando sus credenciales
        /// y genera un token de acceso cuando la autenticación
        /// es exitosa.
        /// </summary>
        /// <param name="signInDto">
        /// DTO que contiene el correo y contraseña
        /// proporcionados por el usuario.
        /// </param>
        /// <returns>
        /// Una operación asíncrona que devuelve un Response<TokenDto>.
        ///
        /// Cuando la autenticación es exitosa, Data contiene
        /// la información del token de acceso.
        /// </returns>
        Task<Response<TokenDto>> SignInAsync(SignInDto signInDto);
    }
}
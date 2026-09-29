using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Domain.Interface
{
    /// <summary>
    /// Define las operaciones de negocio disponibles
    /// para trabajar con usuarios dentro de la capa Domain.
    ///
    /// Esta interfaz define QUÉ operaciones se pueden realizar,
    /// pero no define CÓMO se realizan.
    ///
    /// La implementación se encuentra en UsersDomain.
    /// </summary>
    public interface IUsersDomain
    {
        /// <summary>
        /// Busca un usuario utilizando su correo electrónico.
        /// </summary>
        /// <param name="email">
        /// Correo electrónico utilizado como criterio de búsqueda.
        /// </param>
        /// <returns>
        /// Un User si el usuario existe.
        ///
        /// User? significa que también puede devolver null
        /// cuando no se encuentra ningún usuario.
        ///
        /// Task indica que la operación se ejecuta de forma asíncrona.
        /// </returns>
        Task<User?> GetByEmailAsync(string email);


        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="user">
        /// Entidad User que contiene los datos del usuario.
        /// </param>
        /// <param name="password">
        /// Contraseña proporcionada para el nuevo usuario.
        ///
        /// La contraseña debe ser procesada mediante hashing
        /// antes de almacenarse de forma persistente.
        /// </param>
        /// <returns>
        /// true si el usuario fue creado correctamente;
        /// false si la operación no se realizó.
        /// </returns>
        Task<bool> CreateUserAsync(User user, string password);


        /// <summary>
        /// Comprueba si la contraseña proporcionada corresponde
        /// al usuario indicado.
        /// </summary>
        /// <param name="user">
        /// Usuario cuya contraseña se desea verificar.
        /// </param>
        /// <param name="password">
        /// Contraseña proporcionada durante el inicio de sesión.
        /// </param>
        /// <returns>
        /// true si la contraseña es válida;
        /// false si no coincide.
        /// </returns>
        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
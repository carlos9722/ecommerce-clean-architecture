using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    /// <summary>
    /// Define el contrato del repositorio de usuarios.
    ///
    /// Este repositorio pertenece a la capa Infrastructure
    /// y define las operaciones necesarias para acceder
    /// y modificar los datos de los usuarios.
    ///
    /// La implementación concreta será UsersRepository,
    /// que será la encargada de comunicarse con la base de datos.
    /// </summary>
    public interface IUsersRepository
    {
        /// <summary>
        /// Busca un usuario utilizando su correo electrónico.
        /// </summary>
        /// <param name="email">
        /// Correo electrónico utilizado como criterio de búsqueda.
        /// </param>
        /// <returns>
        /// Un objeto User si el usuario existe.
        ///
        /// User? indica que también puede devolver null
        /// cuando no se encuentra ningún usuario.
        ///
        /// Task indica que la operación se ejecuta de forma asíncrona.
        /// </returns>
        Task<User?> GetByEmailAsync(string email);


        /// <summary>
        /// Crea un nuevo usuario en la fuente de datos.
        /// </summary>
        /// <param name="user">
        /// Entidad User que contiene los datos del usuario.
        /// </param>
        /// <param name="password">
        /// Contraseña proporcionada para el usuario.
        ///
        /// La implementación deberá encargarse de procesar
        /// la contraseña de forma segura antes de almacenarla.
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
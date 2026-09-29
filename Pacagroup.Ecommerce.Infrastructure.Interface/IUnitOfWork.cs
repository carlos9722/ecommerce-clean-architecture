namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    /// <summary>
    /// Define el contrato del Unit of Work.
    ///
    /// El Unit of Work centraliza y proporciona acceso
    /// a los diferentes repositorios de la aplicación.
    ///
    /// De esta forma, las capas superiores pueden acceder
    /// a los repositorios a través de un único punto de entrada.
    ///
    /// IDisposable indica que la implementación del Unit of Work
    /// puede liberar recursos cuando ya no sean necesarios.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Proporciona acceso al repositorio de clientes.
        ///
        /// ICustomersRepository es el contrato que define
        /// las operaciones disponibles para trabajar con Customer.
        ///
        /// El get permite obtener el repositorio desde el Unit of Work,
        /// pero no permite asignarlo directamente desde fuera.
        /// </summary>
        ICustomersRepository Customers { get; }


        /// <summary>
        /// Proporciona acceso al repositorio de usuarios.
        ///
        /// IUsersRepository es el contrato que define
        /// las operaciones disponibles para trabajar con User.
        ///
        /// Por ejemplo:
        /// - Buscar un usuario por email.
        /// - Crear un usuario.
        /// - Comprobar una contraseña.
        ///
        /// El get permite obtener el repositorio desde el Unit of Work,
        /// pero no permite reemplazarlo directamente desde fuera.
        /// </summary>
        IUsersRepository Users { get; }
    }
}
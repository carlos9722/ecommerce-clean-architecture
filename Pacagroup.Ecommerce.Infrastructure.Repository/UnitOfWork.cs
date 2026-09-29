using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    /// <summary>
    /// Implementación del Unit of Work.
    ///
    /// Centraliza el acceso a los diferentes repositorios
    /// utilizados por la aplicación.
    ///
    /// En este caso proporciona acceso a:
    /// - CustomersRepository
    /// - UsersRepository
    ///
    /// Implementa IUnitOfWork, por lo que debe proporcionar
    /// todas las propiedades y métodos definidos por ese contrato.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Repositorio utilizado para realizar operaciones
        /// relacionadas con los clientes.
        ///
        /// El tipo es ICustomersRepository para trabajar
        /// mediante el contrato y no depender directamente
        /// de una implementación concreta.
        ///
        /// El get permite obtener el repositorio,
        /// pero no reemplazarlo desde fuera.
        /// </summary>
        public ICustomersRepository Customers { get; }


        /// <summary>
        /// Repositorio utilizado para realizar operaciones
        /// relacionadas con los usuarios.
        ///
        /// El tipo es IUsersRepository para trabajar
        /// mediante el contrato del repositorio.
        /// </summary>
        public IUsersRepository Users { get; }


        /// <summary>
        /// Constructor de UnitOfWork.
        ///
        /// Recibe los repositorios mediante inyección de dependencias.
        /// .NET se encarga de proporcionar las implementaciones
        /// registradas en el contenedor de DI.
        /// </summary>
        /// <param name="customers">
        /// Repositorio de clientes que será utilizado
        /// por el Unit of Work.
        /// </param>
        /// <param name="users">
        /// Repositorio de usuarios que será utilizado
        /// por el Unit of Work.
        /// </param>
        public UnitOfWork(
            ICustomersRepository customers,
            IUsersRepository users)
        {
            /*
             * Guardamos los repositorios recibidos mediante DI.
             *
             * Después podremos acceder a ellos mediante:
             *
             * Customers → repositorio de clientes.
             * Users     → repositorio de usuarios.
             */
            Customers = customers;
            Users = users;
        }


        /// <summary>
        /// Libera los recursos utilizados por el Unit of Work.
        ///
        /// Este método es obligatorio porque IUnitOfWork
        /// hereda de IDisposable.
        /// </summary>
        public void Dispose()
        {
            /*
             * Indica al recolector de basura (Garbage Collector)
             * que no es necesario ejecutar un finalizador
             * para esta instancia.
             *
             * Importante:
             * SuppressFinalize NO elimina ni libera por sí mismo
             * los repositorios o la conexión de base de datos.
             *
             * En este diseño, las conexiones se crean y liberan
             * dentro de los métodos de los repositorios mediante
             * using/using var.
             */
            System.GC.SuppressFinalize(this);
        }
    }
}
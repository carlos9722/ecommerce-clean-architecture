using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    /// <summary>
    /// Implementación del Unit of Work.
    /// Centraliza el acceso a los repositorios utilizados por la aplicación.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Repositorio utilizado para realizar operaciones sobre Customer.
        /// </summary>
        public ICustomersRepository Customers { get; }

        /// <summary>
        /// Constructor de UnitOfWork.
        /// Recibe ICustomersRepository mediante inyección de dependencias
        /// y lo asigna a la propiedad Customers.
        /// </summary>
        /// <param name="customers">
        /// Repositorio de clientes que será utilizado por el Unit of Work.
        /// </param>
        public UnitOfWork(ICustomersRepository customers)
        {
            Customers = customers;
        }

        /// <summary>
        /// Libera los recursos utilizados por el Unit of Work.
        /// </summary>
        public void Dispose()
        {
            // Indica al recolector de basura que no es necesario ejecutar
            // el finalizador de esta instancia.
            System.GC.SuppressFinalize(this);
        }
    }
}
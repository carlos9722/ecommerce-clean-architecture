namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    /// <summary>
    /// Define el contrato del Unit of Work.
    /// Se encarga de centralizar y proporcionar acceso a los diferentes
    /// repositorios utilizados durante una operación.
    /// 
    /// IDisposable indica que la implementación podrá liberar recursos
    /// cuando ya no sean necesarios.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Proporciona acceso al repositorio de clientes.
        /// 
        /// Al ser una propiedad de tipo ICustomersRepository,
        /// permite utilizar sus operaciones CRUD desde el Unit of Work.
        /// </summary>
        ICustomersRepository Customers { get; }
    }
}
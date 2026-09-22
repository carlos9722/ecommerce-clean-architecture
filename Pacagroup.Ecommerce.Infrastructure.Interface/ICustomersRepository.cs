using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    /// <summary>
    /// Define el contrato de acceso a datos específico para la entidad Customer.
    /// Hereda las operaciones CRUD del repositorio genérico y las especializa
    /// automáticamente para trabajar con Customer.
    /// </summary>
    public interface ICustomersRepository : IGenericRepository<Customer>
    {
        // No necesita declarar nuevamente Insert, Update, Delete, Get, etc.
        // porque los hereda de IGenericRepository<Customer>.
    }
}
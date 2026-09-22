namespace Pacagroup.Ecommerce.Infrastructure.Interface
{
    /// <summary>
    /// Define las operaciones básicas de acceso a datos que pueden
    /// reutilizarse para diferentes tipos de entidades.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de entidad con la que trabajará el repositorio.
    /// 'where T : class' indica que T debe ser una clase.
    /// </typeparam>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>
        /// Inserta una nueva entidad en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad que se desea insertar.</param>
        /// <returns>
        /// Una tarea que devuelve true si la operación fue exitosa.
        /// </returns>
        Task<bool> InsertAsync(T entity);

        /// <summary>
        /// Actualiza una entidad existente en la base de datos.
        /// </summary>
        /// <param name="entity">Entidad con los datos actualizados.</param>
        /// <returns>
        /// Una tarea que devuelve true si la operación fue exitosa.
        /// </returns>
        Task<bool> UpdateAsync(T entity);

        /// <summary>
        /// Elimina una entidad utilizando su identificador.
        /// </summary>
        /// <param name="customerId">Identificador de la entidad.</param>
        /// <returns>
        /// Una tarea que devuelve true si la operación fue exitosa.
        /// </returns>
        Task<bool> DeleteAsync(string customerId);

        /// <summary>
        /// Obtiene una entidad utilizando su identificador.
        /// </summary>
        /// <param name="customerId">Identificador de la entidad.</param>
        /// <returns>
        /// Una tarea que devuelve la entidad encontrada o null si no existe.
        /// El '?' indica que el resultado puede ser null.
        /// </returns>
        Task<T?> GetAsync(string customerId);

        /// <summary>
        /// Obtiene todas las entidades disponibles.
        /// </summary>
        /// <returns>
        /// Una tarea que devuelve una colección de entidades.
        /// </returns>
        Task<IEnumerable<T>> GetAllAsync();
    }
}
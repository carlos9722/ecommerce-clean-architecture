using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Interface;

namespace Pacagroup.Ecommerce.Domain.Core
{
    /// <summary>
    /// Implementa las operaciones de negocio relacionadas con los usuarios.
    ///
    /// Esta clase pertenece a la capa Domain y utiliza IUnitOfWork
    /// para acceder a las operaciones de persistencia sin conocer
    /// directamente detalles de la base de datos.
    /// </summary>
    public class UsersDomain : IUsersDomain
    {
        /*
         * Referencia al UnitOfWork.
         *
         * IUnitOfWork permite acceder a los diferentes repositorios
         * de la aplicación, en este caso al repositorio de Users.
         *
         * readonly significa que esta referencia solo puede asignarse
         * durante la declaración o dentro del constructor.
         */
        private readonly IUnitOfWork _unitOfWork;


        /// <summary>
        /// Constructor de UsersDomain.
        ///
        /// Recibe IUnitOfWork mediante inyección de dependencias.
        /// .NET se encarga de proporcionar automáticamente la
        /// implementación registrada en el contenedor de DI.
        /// </summary>
        /// <param name="unitOfWork">
        /// UnitOfWork utilizado para acceder a los repositorios
        /// de la aplicación.
        /// </param>
        public UsersDomain(IUnitOfWork unitOfWork)
        {
            // Guardamos la dependencia para utilizarla en los métodos.
            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Verifica si la contraseña proporcionada corresponde
        /// al usuario indicado.
        /// </summary>
        /// <param name="user">
        /// Usuario cuya contraseña se desea comprobar.
        /// </param>
        /// <param name="password">
        /// Contraseña proporcionada durante el inicio de sesión.
        /// </param>
        /// <returns>
        /// true si la contraseña es válida;
        /// false si no coincide.
        /// </returns>
        public async Task<bool> CheckPasswordAsync(
            User user,
            string password)
        {
            /*
             * Delegamos la operación al repositorio de Users.
             *
             * Users es la propiedad del UnitOfWork que proporciona
             * acceso a las operaciones relacionadas con usuarios.
             */
            return await _unitOfWork.Users.CheckPasswordAsync(
                user,
                password);
        }


        /// <summary>
        /// Crea un nuevo usuario utilizando los datos proporcionados.
        /// </summary>
        /// <param name="user">
        /// Entidad User con los datos del usuario que se desea crear.
        /// </param>
        /// <param name="password">
        /// Contraseña proporcionada por el usuario.
        ///
        /// Normalmente esta contraseña debe ser procesada mediante
        /// un mecanismo de hashing antes de almacenarse.
        /// </param>
        /// <returns>
        /// true si el usuario fue creado correctamente;
        /// false si la operación no se realizó.
        /// </returns>
        public async Task<bool> CreateUserAsync(
            User user,
            string password)
        {
            /*
             * Delegamos la creación del usuario al repositorio.
             *
             * El Domain no realiza directamente operaciones SQL
             * ni crea conexiones con la base de datos.
             */
            return await _unitOfWork.Users.CreateUserAsync(
                user,
                password);
        }


        /// <summary>
        /// Busca un usuario utilizando su dirección de correo electrónico.
        /// </summary>
        /// <param name="email">
        /// Correo electrónico utilizado para buscar al usuario.
        /// </param>
        /// <returns>
        /// El usuario encontrado o null si no existe.
        ///
        /// El '?' en User? indica que el resultado puede ser null.
        /// </returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            /*
             * Delegamos la búsqueda al repositorio de Users.
             *
             * El resultado puede ser:
             *
             * User → usuario encontrado.
             * null → usuario no encontrado.
             */
            return await _unitOfWork.Users.GetByEmailAsync(email);
        }
    }
}
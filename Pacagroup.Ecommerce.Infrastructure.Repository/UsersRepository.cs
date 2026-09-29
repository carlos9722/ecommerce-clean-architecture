using Dapper;
using Microsoft.AspNetCore.Identity;
using Pacagroup.Ecommerce.Domain.Entity;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using System.Data;

namespace Pacagroup.Ecommerce.Infrastructure.Repository
{
    /// <summary>
    /// Implementación del repositorio de usuarios.
    ///
    /// Esta clase pertenece a Infrastructure y es responsable
    /// de acceder a la base de datos para realizar operaciones
    /// relacionadas con User.
    ///
    /// Utiliza:
    /// - Dapper para ejecutar procedimientos almacenados.
    /// - DapperContext para crear conexiones a SQL Server.
    /// - IPasswordHasher para generar y verificar hashes de contraseñas.
    /// </summary>
    public class UsersRepository : IUsersRepository
    {
        /*
         * Contexto utilizado para crear conexiones con la base de datos.
         *
         * DapperContext contiene la configuración necesaria
         * para obtener una conexión a SQL Server.
         */
        private readonly DapperContext _context;


        /*
         * Servicio encargado de generar y comprobar hashes
         * de contraseñas.
         *
         * IPasswordHasher<User> significa:
         *
         * IPasswordHasher → contrato para trabajar con contraseñas.
         * <User>          → el hashing está asociado a la entidad User.
         *
         * Importante:
         * Nunca debemos almacenar la contraseña original.
         * Se almacena un hash.
         */
        private readonly IPasswordHasher<User> _passwordHasher;


        /// <summary>
        /// Constructor de UsersRepository.
        ///
        /// Recibe sus dependencias mediante inyección de dependencias.
        /// </summary>
        /// <param name="context">
        /// Contexto utilizado para crear conexiones con SQL Server.
        /// </param>
        /// <param name="passwordHasher">
        /// Servicio utilizado para generar y verificar
        /// hashes de contraseñas.
        /// </param>
        public UsersRepository(
            DapperContext context,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }


        /// <summary>
        /// Comprueba si la contraseña proporcionada coincide
        /// con el hash almacenado del usuario.
        /// </summary>
        /// <param name="user">
        /// Usuario que contiene el PasswordHash almacenado.
        /// </param>
        /// <param name="password">
        /// Contraseña escrita por el usuario durante el login.
        /// </param>
        /// <returns>
        /// true si la contraseña coincide con el hash almacenado;
        /// false si no coincide.
        /// </returns>
        public async Task<bool> CheckPasswordAsync(
            User user,
            string password)
        {
            /*
             * VerifyHashedPassword compara:
             *
             * 1. El hash almacenado en la base de datos.
             * 2. La contraseña que acaba de proporcionar el usuario.
             *
             * PasswordHash! utiliza el operador ! para indicar
             * al compilador que en este punto asumimos que
             * PasswordHash no es null.
             */
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash!,
                password);


            /*
             * VerifyHashedPassword devuelve un PasswordVerificationResult.
             *
             * Success significa que la contraseña coincide.
             *
             * Convertimos ese resultado a bool:
             *
             * Success → true
             * Otro resultado → false
             */
            return await Task.FromResult(
                result == PasswordVerificationResult.Success);
        }


        /// <summary>
        /// Crea un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="user">
        /// Entidad User con los datos del usuario.
        /// </param>
        /// <param name="password">
        /// Contraseña original proporcionada durante el registro.
        ///
        /// Esta contraseña no se almacena directamente.
        /// Primero se convierte en un hash.
        /// </param>
        /// <returns>
        /// true si la operación afectó al menos una fila;
        /// false si no se insertó ningún registro.
        /// </returns>
        public async Task<bool> CreateUserAsync(
            User user,
            string password)
        {
            /*
             * Crea una conexión nueva utilizando DapperContext.
             *
             * using var garantiza que la conexión será liberada
             * cuando termine este método.
             */
            using var connection = _context.CreateConnection();


            /*
             * Nombre del procedimiento almacenado que realizará
             * la inserción del usuario.
             */
            var query = "UsersInsert";


            /*
             * DynamicParameters permite construir los parámetros
             * que serán enviados al procedimiento almacenado.
             */
            var parameters = new DynamicParameters();


            /*
             * Generamos un nuevo identificador para el usuario.
             *
             * Guid.NewGuid() genera un GUID único.
             *
             * ToString() convierte ese GUID a texto porque
             * User.Id es de tipo string.
             */
            parameters.Add(
                "Id",
                Guid.NewGuid().ToString());


            /*
             * Agregamos los datos del usuario que serán enviados
             * al procedimiento almacenado.
             */
            parameters.Add("FirstName", user.FirstName);
            parameters.Add("LastName", user.LastName);
            parameters.Add("Email", user.Email);
            parameters.Add("UserName", user.UserName);


            /*
             * IMPORTANTE:
             *
             * La contraseña original NO se guarda.
             *
             * HashPassword() genera un hash seguro utilizando
             * el PasswordHasher de ASP.NET Core Identity.
             *
             * En la base de datos se almacena PasswordHash,
             * no la contraseña original.
             */
            parameters.Add(
                "PasswordHash",
                _passwordHasher.HashPassword(user, password));


            /*
             * ExecuteAsync ejecuta el procedimiento almacenado.
             *
             * Como no estamos obteniendo registros sino realizando
             * un INSERT, utilizamos ExecuteAsync en lugar de QueryAsync.
             *
             * El resultado normalmente representa la cantidad
             * de filas afectadas.
             */
            var result = await connection.ExecuteAsync(
                query,
                param: parameters,
                commandType: CommandType.StoredProcedure);


            /*
             * Si se afectó al menos una fila, consideramos
             * que la operación fue exitosa.
             *
             * Ejemplo:
             *
             * result = 1 → true
             * result = 0 → false
             */
            return result > 0;
        }


        /// <summary>
        /// Busca un usuario utilizando su dirección de correo electrónico.
        /// </summary>
        /// <param name="email">
        /// Correo electrónico utilizado para realizar la búsqueda.
        /// </param>
        /// <returns>
        /// El usuario encontrado o null si no existe.
        /// </returns>
        public async Task<User?> GetByEmailAsync(string email)
        {
            /*
             * Creamos una conexión nueva con SQL Server.
             *
             * using var garantiza que la conexión se libere
             * al terminar el método.
             */
            using var connection = _context.CreateConnection();


            /*
             * Nombre del procedimiento almacenado
             * encargado de buscar el usuario.
             */
            var query = "UsersGetByEmail";


            /*
             * Creamos los parámetros que recibirá
             * el procedimiento almacenado.
             */
            var parameters = new DynamicParameters();

            parameters.Add("Email", email);


            /*
             * QuerySingleOrDefaultAsync<User> ejecuta el procedimiento
             * y convierte el resultado en un objeto User.
             *
             * Puede devolver:
             *
             * User → si encuentra un usuario.
             * null → si no encuentra ninguno.
             *
             * Por eso el método devuelve User?.
             */
            var user = await connection.QuerySingleOrDefaultAsync<User>(
                query,
                param: parameters,
                commandType: CommandType.StoredProcedure);


            // Devuelve el usuario encontrado o null.
            return user;
        }
    }
}
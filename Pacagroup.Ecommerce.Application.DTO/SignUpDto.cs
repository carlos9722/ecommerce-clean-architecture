namespace Pacagroup.Ecommerce.Application.DTO
{
    /// <summary>
    /// DTO utilizado para recibir los datos necesarios
    /// para registrar un nuevo usuario en la aplicación.
    ///
    /// SignUp significa "registro" o "creación de una cuenta".
    /// </summary>
    public sealed record SignUpDto
    {
        /// <summary>
        /// Nombre del usuario.
        ///
        /// string.Empty representa una cadena vacía ("") y se utiliza
        /// como valor inicial para evitar que la propiedad sea null.
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Apellido del usuario.
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico que utilizará el usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Nombre de usuario que utilizará para identificarse
        /// dentro de la aplicación.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña proporcionada durante el registro.
        ///
        /// Esta contraseña debe ser procesada mediante un mecanismo
        /// seguro de hashing antes de almacenarse en la base de datos.
        /// Nunca debe guardarse directamente como texto plano.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
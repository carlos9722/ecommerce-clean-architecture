namespace Pacagroup.Ecommerce.Application.DTO
{
    /// <summary>
    /// DTO utilizado para recibir las credenciales necesarias
    /// para iniciar sesión en la aplicación.
    ///
    /// Un DTO (Data Transfer Object) es un objeto utilizado
    /// para transportar datos entre diferentes capas de la aplicación.
    /// </summary>
    public sealed record SignInDto
    {
        /// <summary>
        /// Correo electrónico utilizado para identificar al usuario.
        ///
        /// string.Empty representa una cadena vacía ("") y se utiliza
        /// como valor inicial para evitar que la propiedad sea null.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña proporcionada por el usuario durante el inicio
        /// de sesión.
        ///
        /// string.Empty representa una cadena vacía ("") y evita
        /// que la propiedad tenga null como valor inicial.
        ///
        /// Importante: esta propiedad contiene la contraseña recibida
        /// en la solicitud y no debe almacenarse directamente en la base
        /// de datos como texto plano.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
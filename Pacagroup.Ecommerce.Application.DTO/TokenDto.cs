namespace Pacagroup.Ecommerce.Application.DTO
{
    /// <summary>
    /// DTO utilizado para devolver al cliente la información
    /// relacionada con el token generado después de una autenticación
    /// exitosa.
    ///
    /// Normalmente este DTO se utiliza cuando el usuario inicia sesión
    /// correctamente y la API genera un token de acceso.
    /// </summary>
    public sealed record TokenDto
    {
        /// <summary>
        /// Token que el cliente utilizará para autenticarse
        /// en las siguientes solicitudes a la API.
        ///
        /// Normalmente contiene un JWT (JSON Web Token).
        ///
        /// Se inicializa con string.Empty ("") para evitar
        /// que la propiedad tenga null como valor inicial.
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;


        /// <summary>
        /// Indica el esquema utilizado para enviar el token
        /// en el encabezado Authorization.
        ///
        /// "Bearer" significa que el cliente enviará el token
        /// de la siguiente manera:
        ///
        /// Authorization: Bearer <token>
        ///
        /// Se establece "Bearer" como valor predeterminado porque
        /// es el esquema utilizado habitualmente con tokens JWT.
        /// </summary>
        public string TokenType { get; set; } = "Bearer";


        /// <summary>
        /// Tiempo, normalmente expresado en segundos, durante el cual
        /// el token será válido antes de expirar.
        ///
        /// Por ejemplo:
        /// ExpiresIn = 3600
        /// significa que el token tiene una duración de 3600 segundos
        /// (1 hora).
        /// </summary>
        public int ExpiresIn { get; set; }
    }
}
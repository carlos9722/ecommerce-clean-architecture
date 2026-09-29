using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Pacagroup.Ecommerce.Domain.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pacagroup.Ecommerce.Transversal.Common
{
    /// <summary>
    /// Implementación del servicio encargado de generar tokens JWT.
    ///
    /// Implementa IJwtService, por lo que debe proporcionar
    /// la operación GenerateToken definida por ese contrato.
    ///
    /// Esta clase utiliza:
    /// - IConfiguration para leer la configuración del JWT.
    /// - Microsoft.IdentityModel.Tokens para crear la clave y firma.
    /// - JwtSecurityToken para construir el token.
    /// - Claims para incluir información del usuario dentro del JWT.
    /// </summary>
    public class JwtService : IJwtService
    {
        /*
         * IConfiguration permite leer valores de configuración
         * provenientes de appsettings.json, variables de entorno,
         * User Secrets, etc.
         */
        private readonly IConfiguration _configuration;


        /// <summary>
        /// Constructor de JwtService.
        ///
        /// IConfiguration se recibe mediante inyección de dependencias.
        /// ASP.NET Core proporciona automáticamente esta instancia.
        /// </summary>
        /// <param name="configuration">
        /// Objeto utilizado para leer la configuración de la aplicación.
        /// </param>
        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /// <summary>
        /// Genera un token JWT para el usuario indicado.
        /// </summary>
        /// <param name="user">
        /// Usuario cuyos datos serán incluidos como claims
        /// dentro del token.
        /// </param>
        /// <returns>
        /// Token JWT representado como una cadena de texto.
        /// </returns>
        public string GenerateToken(User user)
        {
            /*
             * Validamos que el usuario tenga un Id.
             *
             * El Id será utilizado como ClaimTypes.NameIdentifier
             * dentro del JWT.
             *
             * string.IsNullOrWhiteSpace() devuelve true cuando:
             *
             * - La cadena es null.
             * - La cadena está vacía.
             * - La cadena contiene solamente espacios.
             */
            if (string.IsNullOrWhiteSpace(user.Id))
                throw new ArgumentException(
                    "User Id is required.",
                    nameof(user));


            /*
             * ============================================================
             * CREACIÓN DE LA CLAVE DE SEGURIDAD
             * ============================================================
             *
             * Leemos la clave secreta desde la configuración:
             *
             * Jwt:Key
             *
             * Por ejemplo, en appsettings.json:
             *
             * "Jwt": {
             *     "Key": "una-clave-secreta...",
             *     "Issuer": "...",
             *     "Audience": "..."
             * }
             *
             * El operador ! indica al compilador que asumimos
             * que el valor no será null.
             *
             * Encoding.UTF8.GetBytes() convierte el texto de la clave
             * en un arreglo de bytes.
             *
             * SymmetricSecurityKey utiliza esos bytes como clave
             * criptográfica.
             */
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));


            /*
             * Creamos las credenciales utilizadas para firmar el JWT.
             *
             * SecurityAlgorithms.HmacSha256 indica que utilizaremos
             * HMAC-SHA256 como algoritmo de firma.
             *
             * La firma permite comprobar posteriormente que el token
             * fue generado utilizando la clave esperada y que
             * su contenido no fue alterado.
             */
            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);


            /*
             * ============================================================
             * CLAIMS
             * ============================================================
             *
             * Los claims son datos que se incluyen dentro del JWT
             * para representar información relacionada con el usuario
             * o con la autenticación.
             *
             * Aquí estamos incluyendo:
             *
             * NameIdentifier → Id del usuario.
             * Email          → Email del usuario.
             * Name           → Nombre completo.
             */
            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    user.Id),

                new Claim(
                    ClaimTypes.Email,
                    user.Email!),

                new Claim(
                    ClaimTypes.Name,
                    $"{user.FirstName} {user.LastName}")
            };


            /*
             * ============================================================
             * CREACIÓN DEL JWT
             * ============================================================
             *
             * JwtSecurityToken construye el token utilizando:
             *
             * issuer:
             *    Quién emite el token.
             *
             * audience:
             *    Para quién está destinado el token.
             *
             * claims:
             *    Información del usuario que queremos transportar.
             *
             * expires:
             *    Fecha y hora en que el token dejará de ser válido.
             *
             * signingCredentials:
             *    Clave y algoritmo utilizados para firmarlo.
             */
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,

                // El token tendrá una duración de 1 hora.
                expires: DateTime.UtcNow.AddHours(1),

                signingCredentials: credentials
            );


            /*
             * JwtSecurityToken representa el token como un objeto.
             *
             * WriteToken() lo convierte al formato JWT que puede
             * enviarse al cliente como una cadena de texto.
             *
             * Resultado aproximado:
             *
             * eyJhbGciOiJIUzI1NiIs...
             */
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
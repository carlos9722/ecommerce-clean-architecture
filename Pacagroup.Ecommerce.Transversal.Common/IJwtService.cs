using Pacagroup.Ecommerce.Domain.Entity;

namespace Pacagroup.Ecommerce.Transversal.Common
{
    /// <summary>
    /// Define el contrato para el servicio encargado
    /// de generar tokens JWT para los usuarios autenticados.
    ///
    /// La interfaz define QUÉ debe hacer el servicio,
    /// pero no define CÓMO se genera el token.
    ///
    /// La implementación concreta será JwtService.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Genera un token JWT para el usuario indicado.
        ///
        /// El token normalmente contiene información del usuario
        /// y datos de seguridad como:
        /// - Identificador del usuario.
        /// - Email o nombre de usuario.
        /// - Fecha de expiración.
        /// - Claims adicionales.
        /// </summary>
        /// <param name="user">
        /// Usuario autenticado para el cual se generará el token.
        /// </param>
        /// <returns>
        /// Una cadena de texto que contiene el token JWT generado.
        /// </returns>
        string GenerateToken(User user);
    }
}
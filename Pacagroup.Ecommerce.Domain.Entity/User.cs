namespace Pacagroup.Ecommerce.Domain.Entity;

/// <summary>
/// Representa un usuario dentro del dominio de la aplicación.
/// </summary>
public class User
{
    /// <summary>
    /// Identificador único del usuario.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Nombre del usuario.
    /// 
    /// string.Empty representa una cadena vacía ("").
    /// Se utiliza para evitar que la propiedad tenga null
    /// como valor inicial.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Apellido del usuario.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Correo electrónico del usuario.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Nombre de usuario utilizado para identificarse
    /// dentro de la aplicación.
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// Hash de la contraseña del usuario.
    ///
    /// Se inicializa con string.Empty ("") para evitar null.
    /// Nunca debe almacenarse la contraseña original en texto plano.
    /// </summary>
    public string PasswordHash { get; set; } = string.Empty;
}
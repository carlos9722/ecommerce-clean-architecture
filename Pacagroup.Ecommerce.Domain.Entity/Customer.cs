namespace Pacagroup.Ecommerce.Domain.Entity
{
    /// <summary>
    /// Representa un cliente dentro del dominio del ecommerce.
    /// Contiene información de identificación, contacto y ubicación.
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Identificador único del cliente.
        /// El tipo debe coincidir con el identificador definido en la BD.
        /// Por ejemplo, podría ser string o Guid según el diseño.
        /// El signo '?' indica que la propiedad puede contener null.
        /// Esto forma parte de Nullable Reference Types de C#.
        /// </summary>
        public string? CustomerId { get; set; }

        /// <summary>
        /// Nombre de la empresa a la que pertenece el cliente.
        /// El '?' indica que la propiedad puede contener null.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Nombre de la persona que sirve como contacto del cliente.
        /// </summary>
        public string? ContactName { get; set; }

        /// <summary>
        /// Cargo o posición de la persona de contacto.
        /// </summary>
        public string? ContactTitle { get; set; }

        /// <summary>
        /// Dirección física del cliente.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Ciudad donde se encuentra el cliente.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Región, departamento o estado del cliente.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Código postal de la dirección del cliente.
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// País donde se encuentra el cliente.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Número telefónico del cliente.
        /// Se utiliza string porque un teléfono puede contener
        /// símbolos, espacios, prefijos internacionales o extensiones.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Número de fax del cliente, si está disponible.
        /// Se mantiene como string por la misma razón que Phone.
        /// </summary>
        public string? Fax { get; set; }
    }
}
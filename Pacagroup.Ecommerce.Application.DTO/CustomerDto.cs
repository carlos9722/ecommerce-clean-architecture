namespace Pacagroup.Ecommerce.Application.DTO
{
    /// <summary>
    /// Objeto utilizado para transportar los datos de un cliente entre
    /// las diferentes capas de la aplicación.
    ///
    /// El DTO sirve como una "caja de datos": contiene la información
    /// que una capa necesita recibir o enviar, sin tener que pasar
    /// directamente la entidad Customer del Domain.
    ///
    /// Esto permite separar los datos que se exponen o transfieren
    /// de la entidad que utiliza internamente el Domain.
    /// </summary>
    public sealed record CustomerDto
    {
        /// <summary>
        /// Identificador único del cliente.
        /// El '?' indica que puede contener null.
        /// </summary>
        public string? CustomerId { get; set; }

        /// <summary>
        /// Nombre de la empresa del cliente.
        /// </summary>
        public string? CompanyName { get; set; }

        /// <summary>
        /// Nombre de la persona de contacto.
        /// </summary>
        public string? ContactName { get; set; }

        /// <summary>
        /// Cargo de la persona de contacto.
        /// </summary>
        public string? ContactTitle { get; set; }

        /// <summary>
        /// Dirección del cliente.
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Ciudad donde se encuentra el cliente.
        /// </summary>
        public string? City { get; set; }

        /// <summary>
        /// Región o estado donde se encuentra el cliente.
        /// </summary>
        public string? Region { get; set; }

        /// <summary>
        /// Código postal del cliente.
        /// </summary>
        public string? PostalCode { get; set; }

        /// <summary>
        /// País donde se encuentra el cliente.
        /// </summary>
        public string? Country { get; set; }

        /// <summary>
        /// Número telefónico del cliente.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// Número de fax del cliente.
        /// </summary>
        public string? Fax { get; set; }
    }
}
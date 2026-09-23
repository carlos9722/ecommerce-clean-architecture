namespace Pacagroup.Ecommerce.Transversal.Common
{
    /// <summary>
    /// Modelo genérico utilizado para estandarizar las respuestas
    /// de la aplicación.
    ///
    /// Permite devolver en una misma estructura:
    /// - Los datos de la operación mediante Data.
    /// - Si la operación fue exitosa mediante IsSuccess.
    /// - Un mensaje informativo mediante Message.
    ///
    /// T representa el tipo de dato que se devolverá en Data.
    /// Por ejemplo:
    /// Response<Customer>           → Data será un Customer.
    /// Response<List<Customer>>     → Data será una lista de Customer.
    /// Response<bool>               → Data será un true/false.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de dato que contendrá la propiedad Data.
    /// </typeparam>
    public class Response<T>
    {
        /// <summary>
        /// Contiene los datos que devuelve la operación.
        /// El tipo de dato depende de T.
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Indica si la operación se realizó correctamente.
        ///
        /// true  → operación exitosa.
        /// false → operación no exitosa.
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Contiene un mensaje relacionado con el resultado
        /// de la operación.
        ///
        /// Se inicializa como string.Empty para evitar que
        /// la propiedad comience con un valor null.
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
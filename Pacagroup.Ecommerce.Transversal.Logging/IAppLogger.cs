namespace Pacagroup.Ecommerce.Transversal.Logging
{
    /// <summary>
    /// Define el contrato para el sistema de logging personalizado
    /// de la aplicación.
    ///
    /// Las clases que implementen esta interfaz deben proporcionar
    /// los métodos necesarios para registrar diferentes niveles
    /// de mensajes.
    /// </summary>
    /// <typeparam name="T">
    /// Tipo de la clase desde la cual se realiza el logging.
    ///
    /// Por ejemplo:
    /// IAppLogger<AuthApplication>
    /// </typeparam>
    public interface IAppLogger<T>
    {
        /// <summary>
        /// Registra un mensaje de nivel Information.
        ///
        /// Se utiliza para registrar información sobre el
        /// funcionamiento normal de la aplicación.
        /// </summary>
        /// <param name="message">
        /// Mensaje que se desea registrar.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// del mensaje.
        /// </param>
        void LogInformation(string message, params object[] args);


        /// <summary>
        /// Registra un mensaje de nivel Warning.
        ///
        /// Se utiliza para situaciones que requieren atención
        /// pero que no necesariamente representan un error.
        /// </summary>
        /// <param name="message">
        /// Mensaje que describe la advertencia.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// del mensaje.
        /// </param>
        void LogWarning(string message, params object[] args);


        /// <summary>
        /// Registra un mensaje de nivel Error.
        ///
        /// Se utiliza cuando ocurre un error durante la ejecución
        /// de una operación.
        /// </summary>
        /// <param name="message">
        /// Mensaje que describe el error.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// del mensaje.
        /// </param>
        void LogError(string message, params object[] args);


        /// <summary>
        /// Registra un error junto con la excepción que lo produjo.
        ///
        /// Permite conservar información adicional de la excepción,
        /// como el mensaje y el stack trace.
        /// </summary>
        /// <param name="ex">
        /// Excepción que produjo el error.
        /// </param>
        /// <param name="message">
        /// Mensaje que describe el error.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// del mensaje.
        /// </param>
        void LogError(Exception ex, string message, params object[] args);


        /// <summary>
        /// Registra un mensaje de nivel Debug.
        ///
        /// Se utiliza principalmente para información detallada
        /// útil durante el desarrollo y diagnóstico.
        /// </summary>
        /// <param name="message">
        /// Mensaje que se desea registrar.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// del mensaje.
        /// </param>
        void LogDebug(string message, params object[] args);
    }
}
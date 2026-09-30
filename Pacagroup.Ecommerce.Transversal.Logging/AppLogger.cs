using Microsoft.Extensions.Logging;

namespace Pacagroup.Ecommerce.Transversal.Logging
{
    /// <summary>
    /// Implementa IAppLogger y proporciona una capa propia
    /// para registrar mensajes de logging en la aplicación.
    ///
    /// Utiliza ILogger de Microsoft internamente para enviar
    /// los mensajes al sistema de logging configurado.
    /// </summary>
    public class AppLogger<T> : IAppLogger<T>
    {
        // Logger proporcionado por el sistema de inyección de dependencias.
        //
        // T identifica la clase desde la cual se están registrando
        // los mensajes.
        //
        // Ejemplo:
        //
        // AppLogger<AuthApplication>
        //        ↓
        // ILogger<AuthApplication>
        private readonly ILogger<T> _logger;


        /// <summary>
        /// Constructor que recibe el logger mediante
        /// inyección de dependencias.
        /// </summary>
        /// <param name="logger">
        /// Logger de Microsoft asociado al tipo T.
        /// </param>
        public AppLogger(ILogger<T> logger)
        {
            _logger = logger;
        }


        /// <summary>
        /// Registra un mensaje de nivel Debug.
        ///
        /// Se utiliza principalmente para información detallada
        /// útil durante el desarrollo o diagnóstico.
        /// </summary>
        /// <param name="message">
        /// Mensaje que se desea registrar.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// incluidos en el mensaje.
        /// </param>
        public void LogDebug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }


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
        /// incluidos en el mensaje.
        /// </param>
        public void LogError(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }


        /// <summary>
        /// Registra un error junto con la excepción que lo produjo.
        ///
        /// Permite conservar información adicional de la excepción,
        /// como su mensaje y stack trace.
        /// </summary>
        /// <param name="ex">
        /// Excepción que produjo el error.
        /// </param>
        /// <param name="message">
        /// Mensaje que describe el error.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// incluidos en el mensaje.
        /// </param>
        public void LogError(Exception ex, string message, params object[] args)
        {
            _logger.LogError(ex, message, args);
        }


        /// <summary>
        /// Registra un mensaje de nivel Information.
        ///
        /// Se utiliza para registrar información relevante
        /// sobre el funcionamiento normal de la aplicación.
        /// </summary>
        /// <param name="message">
        /// Mensaje que se desea registrar.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// incluidos en el mensaje.
        /// </param>
        public void LogInformation(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }


        /// <summary>
        /// Registra un mensaje de nivel Warning.
        ///
        /// Se utiliza para situaciones que no necesariamente
        /// detienen la aplicación, pero requieren atención.
        /// </summary>
        /// <param name="message">
        /// Mensaje que describe la advertencia.
        /// </param>
        /// <param name="args">
        /// Valores utilizados para completar los placeholders
        /// incluidos en el mensaje.
        /// </param>
        public void LogWarning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }
    }
}
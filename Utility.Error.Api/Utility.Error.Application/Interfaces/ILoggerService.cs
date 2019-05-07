namespace Utility.Error.Application.Interfaces
{
    /// <summary>
    /// Write log messages to one or more sinks.
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Writes a pre-formatted Debug severity log message.
        /// </summary>
        void Debug(string message);

        /// <summary>
        /// Writes a pre-formatted Information severity log message for a specific event.
        /// </summary>
        void Info(int eventId, string message);

        /// <summary>
        /// Writes a pre-formatted Warning severity log message for a specific event.
        /// </summary>
        void Warn(int eventId, string message);

        /// <summary>
        /// Writes a pre-formatted Error severity log message for a specific event.
        /// </summary>
        void Error(int eventId, string message);

        /// <summary>
        /// Writes a pre-formatted Critical severity log message for a specific event.
        /// </summary>
        void Critical(int eventId, string message);

        /// <summary>
        /// Writes a custom error into specific error sink.
        /// </summary>
        /// <param name="errorMsg"></param>
        void CustomError(Domain.Entities.Error errorMsg);
    }
}

using System.Diagnostics;
using Utility.Error.Application.Interfaces;

namespace Utility.Error.Infrastructure.Logging
{
    /// <summary>
    /// The log sink that writes to the debugger stream.
    /// </summary>
    public class TraceLoggerService : ILoggerService
    {
        // The sink.
        private static readonly DefaultTraceListener DebugWriter = new DefaultTraceListener();

        // Interface Implementation.
        #region InterfaceImplementation

        /// <summary>
        /// Writes a pre-formatted Debug severity log message.
        /// </summary>
        public void Debug(string message)
        {
            DebugWriter.WriteLine($"DBG: {message}");
        }

        /// <summary>
        /// Writes a pre-formatted Information severity log message for a specific event.
        /// </summary>
        public void Info(int eventId, string message)
        {
            DebugWriter.WriteLine($"INF: {message}");
        }

        /// <summary>
        /// Writes a pre-formatted Warning severity log message for a specific event.
        /// </summary>
        public void Warn(int eventId, string message)
        {
            DebugWriter.WriteLine($"WRN: {message}");
        }

        /// <summary>
        /// Writes a pre-formatted Error severity log message for a specific event.
        /// </summary>
        public void Error(int eventId, string message)
        {
            DebugWriter.WriteLine($"ERR: {message}");
        }

        /// <summary>
        /// Writes a pre-formatted Error severity log message for a specific event.
        /// </summary>
        public void Critical(int eventId, string message)
        {
            DebugWriter.WriteLine($"CRT: {message}");
        }

        /// <summary>
        /// Writes a pre-formatted Error severity log message using supplied error details model.
        /// </summary>
        /// <param name="errorLog"></param>
        public void CustomError(Domain.Entities.Error errorLog)
        {
            DebugWriter.WriteLine($"CUS: Identifier:{errorLog.Identifier}");
            DebugWriter.WriteLine($"CUS: DateTimeCreated:{errorLog.DateTimeCreated}");
            DebugWriter.WriteLine($"CUS: Details:{errorLog.Details}");
        }

        #endregion
    }
}

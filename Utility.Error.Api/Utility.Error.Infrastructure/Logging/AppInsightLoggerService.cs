using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationInsights;
using TraceSeverityLevel = Microsoft.ApplicationInsights.DataContracts.SeverityLevel;

using Utility.Error.Application.Interfaces;
using Utility.Error.Common.Domain;

namespace Utility.Error.Infrastructure.Logging
{
    /// <summary>
    /// AppInsightsLoggerService.
    /// </summary>
    public class AppInsightLoggerService : ILoggerService, IDisposable
    {
        private readonly TelemetryClient _client;
        private readonly string AzureAppInsightsDefaultEventName = "Brewin.Azure.Errors";

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        public AppInsightLoggerService()
        {
            _client = new TelemetryClient();
        }

        #endregion

        // Interface Implementation.
        #region InterfaceImplementation

        /// <summary>
        /// Debug.
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            _client.TrackTrace($"DBG: {message}", TraceSeverityLevel.Verbose);
        }

        /// <summary>
        /// Info.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        public void Info(int eventId, string message)
        {
            _client.TrackTrace($"INF: {message}", TraceSeverityLevel.Information);
        }

        /// <summary>
        /// Warn.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        public void Warn(int eventId, string message)
        {
            _client.TrackTrace($"WRN: {message}", TraceSeverityLevel.Warning);
        }

        /// <summary>
        /// Error.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        public void Error(int eventId, string message)
        {
            _client.TrackTrace($"ERR: {message}", TraceSeverityLevel.Error);
        }

        /// <summary>
        /// Critcal.
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        public void Critical(int eventId, string message)
        {
            _client.TrackTrace($"CRT: {message}", TraceSeverityLevel.Error);
        }

        /// <summary>
        /// CustomError
        /// </summary>
        /// <param name="errorLog"></param>
        public void CustomError(Domain.Entities.Error errorLog)
        {
            // Initialise Source Name.
            var source = string.IsNullOrEmpty(errorLog.Source) ? AzureAppInsightsDefaultEventName : errorLog.Source;

            // Initialise Tracking Properties.
            var properties = new Dictionary<string, string>
            {
                {"Identifier", errorLog.Identifier},
                {"Category", errorLog.Category},
                {"Severity", errorLog.Severity},
                {"Source", source},
                {"ErrorCode", errorLog.ErrorCode},
                {"DateTimeCreated", errorLog.DateTimeCreated.ToString("s")},
                {"Details", errorLog.Details},
                {"StackTrace", string.IsNullOrEmpty(errorLog.StackTrace) ? string.Empty : errorLog.StackTrace }
            };

            // Track Event(s).
            _client.TrackEvent(source, properties);
            TraceThroughApplicationInsights(errorLog);
        }

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion

        // Private Methods.
        #region PrivateMethods

        /// <summary>
        /// Trace Through Application Insights.
        /// </summary>
        /// <param name="errorLog"></param>
        private void TraceThroughApplicationInsights(Domain.Entities.Error errorLog)
        {
            var output = new StringBuilder();

            // Build Output Message.
            output.Append($"Identifier:[{errorLog.Identifier}]...\n");
            output.Append($"  Category:[{errorLog.Category}]...\n");
            output.Append($"    Source:[{errorLog.Source}]...\n");
            output.Append($" ErrorCode:[{errorLog.ErrorCode}]...\n");
            output.Append($"   Details:[{errorLog.Details}]...\n");
            output.Append($"StackTrace:[{errorLog.StackTrace}].\n");

            if (!Enum.TryParse(errorLog.Severity, true, out LocalSettings.ErrorSeverity result))
            {
                return;
            }

            switch (result)
            {
                case LocalSettings.ErrorSeverity.Debug:
                {
                    Debug(output.ToString());
                    break;
                }
                case LocalSettings.ErrorSeverity.Informational:
                {
                    Info(0, output.ToString());
                    break;
                }
                case LocalSettings.ErrorSeverity.Warning:
                {
                    Warn(0, output.ToString());
                    break;
                }
                case LocalSettings.ErrorSeverity.Error:
                {
                    Error(0, output.ToString());
                    break;
                }
                case LocalSettings.ErrorSeverity.Critical:
                {
                    Critical(0, output.ToString());
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        #endregion
    }
}


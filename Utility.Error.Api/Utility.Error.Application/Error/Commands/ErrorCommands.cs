using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Utility.Error.Application.Error.Models;
using Utility.Error.Application.Interfaces;

namespace Utility.Error.Application.Error.Commands
{
    /// <summary>
    /// Error - Notifications/Handlers.
    /// </summary>
    public static class ErrorCommands
    {
        // MediatR Notification(s).
        #region MediatRNotification

        /// <summary>
        /// Command.
        /// </summary>
        public class ErrorLog : INotification
        {
            public ErrorDetailModel Message { get; set; }
        }

        #endregion

        // MediatR Notification Handler(s).
        #region MediatRNotificationHandler

        /// <summary>
        /// ErrorLogHandler.
        /// </summary>
        public class ErrorLogHandler : INotificationHandler<ErrorLog>
        {
            private readonly ILoggerService _loggerService;

            // Public Methods.
            #region PublicMethods

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="loggerService"></param>
            public ErrorLogHandler(ILoggerService loggerService)
            {
                _loggerService = loggerService;
            }

            #endregion

            // Interface Implementation.
            #region InterfaceImplementation

            /// <summary>
            /// Handle.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public Task Handle(ErrorLog request, CancellationToken cancellationToken)
            {
                // Initialise Entity.
                var entity = ErrorDetailModel.Persist(request.Message);
                entity.ErrorChannel = "LOG";

                // Log.
                _loggerService.CustomError(entity);

                return Task.FromResult(true);
            }

            #endregion
        }

        /// <summary>
        /// ErrorLogDbHandler.
        /// </summary>
        public class ErrorLogDbHandler : INotificationHandler<ErrorLog>
        {
            private readonly IUnitOfWork _unitofwork;

            // Public Methods.
            #region PublicMethods

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="unitofwork"></param>
            public ErrorLogDbHandler(IUnitOfWork unitofwork)
            {
                _unitofwork = unitofwork;
            }

            #endregion

            // Interface Implementation.
            #region InterfaceImplementation

            /// <summary>
            /// Handle.
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public Task Handle(ErrorLog request, CancellationToken cancellationToken)
            {
                // Initialise Entity.
                var entity = ErrorDetailModel.Persist(request.Message);
                entity.ErrorChannel = "DB";

                // Persist.
                _unitofwork.Errors.Add(entity);
                _unitofwork.Complete();

                return Task.FromResult(true);
            }

            #endregion
        }

        #endregion
    }
}

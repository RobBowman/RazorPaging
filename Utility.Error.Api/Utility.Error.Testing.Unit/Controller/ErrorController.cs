using MediatR;

using Utility.Error.Application.Error.Models;

namespace Utility.Error.Testing.Unit.Controller
{
    /// <summary>
    /// Error Controller.
    /// </summary>
    public class ErrorController : BaseController
    {
        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="mediator"></param>
        public ErrorController(IMediator mediator) : base(mediator) { }

        #endregion

        /// <summary>
        /// Post Error.
        /// </summary>
        /// <param name="request"></param>
        public void PostError(ErrorDetailModel request)
        {
            Mediator.Publish(new Application.Error.Commands.ErrorCommands.ErrorLog() { Message = request });
        }

        /// <summary>
        /// Get Error By Identifier.
        /// </summary>
        /// <param name="identifier"></param>
        public ErrorDetailModel GetErrorByIdentifier(string identifier)
        {
            return Mediator.Send(new Application.Error.Queries.ByIdentifier.Query() { Identifier = identifier}).Result;
        }
    }
}

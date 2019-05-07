using MediatR;

namespace Utility.Error.Testing.Unit.Controller
{
    public abstract class BaseController
    {
        protected BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }
    }
}

using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

using Utility.Error.Application.Error.Models;
using Utility.Error.Application.Exceptions;
using Utility.Error.Application.Interfaces;

namespace Utility.Error.Application.Error.Queries
{
    /// <summary>
    /// ByIdentifier (QUERY) - Query/Handler/Validator.
    /// </summary>
    public static class ByIdentifier
    {
        // MediatR Query.
        #region MediatRQuery

        /// <summary>
        /// ByIdQuery.
        /// </summary>
        public class Query : IRequest<ErrorDetailModel>
        {
            public string Identifier { get; set; }
        }

        #endregion

        // MediatR Handler.
        #region MediatRHandler

        /// <summary>
        /// By Digital Service Id and Subscription Key Handler.
        /// </summary>
        public class Handler : IRequestHandler<Query, ErrorDetailModel>
        {
            private readonly IUnitOfWork _unitofwork;

            // Public Methods.
            #region PublicMethods

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="unitofwork"></param>
            public Handler(IUnitOfWork unitofwork)
            {
                _unitofwork = unitofwork;
            }

            #endregion

            // Interface Implementation.
            #region InterfaceImplementation

            /// <summary>
            /// Handle
            /// </summary>
            /// <param name="request"></param>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public Task<ErrorDetailModel> Handle(Query request, CancellationToken cancellationToken)
            {
                // Search for 
                var entity = _unitofwork.Errors.GetErrorByIdentifier(request.Identifier);

                // If entity found return Model.
                return null == entity
                    ? throw new NotFoundException(nameof(Domain.Entities.Error), request.Identifier)
                    : Task.FromResult(ErrorDetailModel.Create(entity));
            }

            #endregion
        }

        #endregion

        // MediatR Validator.
        #region MediatRValidator

        /// <summary>
        /// Validator.
        /// </summary>
        public class Validator : AbstractValidator<Query>
        {
            /// <summary>
            /// Constructor.
            /// </summary>
            public Validator()
            {
                RuleFor(v => v.Identifier).NotEmpty();
            }
        }

        #endregion
    }
}

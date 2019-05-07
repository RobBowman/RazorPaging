using System.Threading;
using System.Threading.Tasks;
using MediatR;

using Utility.Error.Application.Interfaces;

namespace Utility.Error.Application.Behaviours
{
    /// <summary>
    /// RequestCacheBehaviour.
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class RequestCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : class, ICachableRequest<TResponse>
    {
        private readonly ILoggerService _logger;
        private readonly ICacheProvider _cacheProvider;

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cacheProvider"></param>
        /// <param name="logger"></param>
        public RequestCacheBehaviour(ICacheProvider cacheProvider, ILoggerService logger)
        {
            _cacheProvider = cacheProvider;
            _logger = logger;
        }

        /// <summary>
        /// Handle.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            // Is the value cached?
            var cacheKey = request.GetCacheKey();
            var cachedResponse = (TResponse)_cacheProvider.Get(cacheKey);

            if (null != cachedResponse)
            {
                _logger.Debug($"returning cached response for cachedkey: {cacheKey} request: {@request}");
                return cachedResponse;
            }

            var response = await next();
            _cacheProvider.Add(cacheKey, response);
            return response;
        }

        #endregion
    }
}

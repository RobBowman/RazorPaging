using MediatR;

namespace Utility.Error.Application.Interfaces
{
    /// <summary>
    /// ICachableRequest Interface.
    /// </summary>
    /// <typeparam name="TResponse"></typeparam>
    public interface ICachableRequest<out TResponse> : IRequest<TResponse>
    {
        string GetCacheKey();
    }
}

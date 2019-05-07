using System;

using Utility.Error.Application.Error.Interfaces;

namespace Utility.Error.Application.Interfaces
{
    /// <summary>
    /// Unit  of Work for Management of Data Retrival and Persistence.
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IErrorRepository Errors { get; }

        int Complete();
    }
}

using Utility.Error.Application.Interfaces;

namespace Utility.Error.Application.Error.Interfaces
{
    /// <summary>
    /// IErrorRepository
    /// 
    /// Define data concerns related to errors.
    /// </summary>
    public interface IErrorRepository : IRepository<Domain.Entities.Error>
    {
        // By Identifier.
        Domain.Entities.Error GetErrorByIdentifier(string identifier);
    }
}

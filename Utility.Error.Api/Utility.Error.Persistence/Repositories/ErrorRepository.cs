using System.Linq;
using Microsoft.EntityFrameworkCore;

using Utility.Error.Application.Error.Interfaces;

namespace Utility.Error.Persistence.Repositories
{
    /// <summary>
    /// Error Repository.
    /// </summary>
    public class ErrorRepository : Repository<Domain.Entities.Error>, IErrorRepository
    {
        // Public Methods.
        #region PublicMethods
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public ErrorRepository(DbContext context) : base(context) { }

        #endregion

        // Interface Implementation.
        #region InterfaceImplementation

        /// <summary>
        /// Get Error By Identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns></returns>
        public Domain.Entities.Error GetErrorByIdentifier(string identifier)
        {
            return SearchFor(c => c.Identifier == identifier).SingleOrDefault();
        }

        #endregion
    }
}

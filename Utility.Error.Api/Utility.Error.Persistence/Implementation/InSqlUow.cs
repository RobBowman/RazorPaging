using Utility.Error.Application.Interfaces;
using Utility.Error.Application.Error.Interfaces;
using Utility.Error.Persistence.Repositories;

namespace Utility.Error.Persistence.Implementation
{
    /// <summary>
    /// InSqlUow
    /// </summary>
    public class InSqlUow : IUnitOfWork
    {
        private readonly ErrorsDbContext _context;

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context"></param>
        public InSqlUow(ErrorsDbContext context)
        {
            _context = context;
            Errors = new ErrorRepository(_context);
        }

        #endregion

        // Public Properties.
        #region PublicProperties

        public IErrorRepository Errors { get; }

        #endregion

        // Interface Implementation.
        #region InterfaceImplementation

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }

        /// <summary>
        /// Complete.
        /// </summary>
        /// <returns></returns>
        public int Complete()
        {
            _context.SaveChanges();
            return 0;
        }

        #endregion
    }
}

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Utility.Error.Persistence
{
    /// <summary>
    /// Mock Db Context.
    /// </summary>
    public sealed class MockDbContext : DbContext
    {
        // Public Method
        #region PublicMethods.

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public MockDbContext(DbContextOptions<MockDbContext> options) : base(options)
        {
            Initialise();
        }

        #endregion

        // Protected Method
        #region ProtectedMethods.

        /// <summary>
        /// On Modal Creating.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Error>().Property(u => u.Id).ValueGeneratedOnAdd();
        }

        #endregion

        // Public Properties.
        #region PublicProperties

        public DbSet<Domain.Entities.Error> Errors { get; set; }

        #endregion

        // Private Method.
        #region PrivateMethods

        /// <summary>
        /// Initialise.
        /// </summary>
        private void Initialise()
        {
            if (Set<Domain.Entities.Error>().Any())
            {
                return;
            }

            var dateTimeSeeding = new DateTime(2000, 1, 1, 0, 0, 0);

            // Sample Data for Internal Testing.
            Set<Domain.Entities.Error>().Add(new Domain.Entities.Error()
            {
                Identifier = "d11337c1-d222-49b6-aaa0-ff443b6ddd36",
                Acknowledged = false,
                Actioned = false,
                Category = "Application",
                ErrorChannel = string.Empty,
                DateTimeCreated = dateTimeSeeding,
                DateTimeInserted = dateTimeSeeding,
                DateTimeUpdated = dateTimeSeeding,
                Severity = "Critical",
                Details = "Somethings gone wrong!",
                ErrorCode = "1000",
                Source = "Atomic.Service.Fca",
                StackTrace = string.Empty
                
            });
            Set<Domain.Entities.Error>().Add(new Domain.Entities.Error()
            {
                Identifier = "01d6137f-0904-4138-8ecb-49271021cc8b",
                Acknowledged = false,
                Actioned = false,
                ErrorChannel = string.Empty,
                Category = "Application",
                DateTimeCreated = dateTimeSeeding,
                DateTimeInserted = dateTimeSeeding,
                DateTimeUpdated = dateTimeSeeding,
                Severity = "Critical",
                Details = "Somethings gone wrong!",
                ErrorCode = "1100",
                Source = "Atomic.Service.Fiscal",
                StackTrace = string.Empty
            });
            Set<Domain.Entities.Error>().Add(new Domain.Entities.Error()
            {
                Identifier = "38fd3dcb-ced0-45c6-b3a7-3c3b561761b1",
                Acknowledged = false,
                Actioned = false,
                ErrorChannel = string.Empty,
                Category = "Application",
                DateTimeCreated = dateTimeSeeding,
                DateTimeInserted = dateTimeSeeding,
                DateTimeUpdated = dateTimeSeeding,
                Severity = "Critical",
                Details = "Somethings gone wrong!",
                ErrorCode = "2000",
                Source = "Utility.Service.Lookup",
                StackTrace = string.Empty
            });
            Set<Domain.Entities.Error>().Add(new Domain.Entities.Error()
            {
                Identifier = "9e31302c-c56b-4b0c-b185-c810f3c0f28e",
                Acknowledged = false,
                Actioned = false,
                ErrorChannel = string.Empty,
                Category = "Application",
                DateTimeCreated = dateTimeSeeding,
                DateTimeInserted = dateTimeSeeding,
                DateTimeUpdated = dateTimeSeeding,
                Severity = "Critical",
                Details = "Somethings gone wrong!",
                ErrorCode = "2100",
                Source = "Utility.Service.Reference",
                StackTrace = string.Empty
            });

            SaveChanges();
        }

        #endregion
    }
}
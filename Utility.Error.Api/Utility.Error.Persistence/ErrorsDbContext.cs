using System;
using Microsoft.EntityFrameworkCore;

namespace Utility.Error.Persistence
{
    /// <summary>
    /// Errors Db Context.
    /// </summary>
    public class ErrorsDbContext : DbContext
    {
        private const string DbErrorsTable = "Error";

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        public ErrorsDbContext()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="options"></param>
        public ErrorsDbContext(DbContextOptions<ErrorsDbContext> options) : base(options)
        {
        }

        #endregion

        // Protected Methods.
        #region ProtectedMethods

        /// <summary>
        /// On Configuring.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("options builder is not configured!");
            }
        }

        /// <summary>
        /// On Modal Creating.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            // Set Default Schema.
            modelBuilder.HasDefaultSchema("dbo");

            // Error Table.
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Id).HasColumnName("Id");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Identifier).HasColumnName("Identifier");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Acknowledged).HasColumnName("Acknowledged");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Actioned).HasColumnName("Actioned");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.ErrorChannel).HasColumnName("ErrorChannel");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.DateTimeCreated).HasColumnName("DateTimeCreated");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.DateTimeInserted).HasColumnName("DateTimeInserted");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.DateTimeUpdated).HasColumnName("DateTimeUpdated");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Category).HasColumnName("Category");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Severity).HasColumnName("Severity");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Details).HasColumnName("Details");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.ErrorCode).HasColumnName("ErrorCode");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.Source).HasColumnName("Source");
            modelBuilder.Entity<Domain.Entities.Error>().ToTable(DbErrorsTable).Property(v => v.StackTrace).HasColumnName("StackTrace");
        }

        #endregion
    }
}

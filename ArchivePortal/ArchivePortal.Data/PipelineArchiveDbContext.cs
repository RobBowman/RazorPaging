using ArchivePortal.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ArchivePortal.Data
{
    public class PipelineArchiveDbContext : DbContext
    {
        public PipelineArchiveDbContext(DbContextOptions<PipelineArchiveDbContext> options)
            : base(options)
        {

        }

        public DbSet<PipelineArchive> PipelineArchives { get; set; }
    }
}

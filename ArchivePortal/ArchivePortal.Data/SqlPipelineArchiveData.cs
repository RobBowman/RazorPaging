using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchivePortal.Core;

namespace ArchivePortal.Data
{
    public class SqlPipelineArchiveData : IPipelineArchiveData
    {
        private readonly PipelineArchiveDbContext db;

        public SqlPipelineArchiveData(PipelineArchiveDbContext db)
        {
            this.db = db;
        }


        public int GetCountOfPipelineArchives()
        {
            return db.PipelineArchives.Count();
        }

        public IEnumerable<PipelineArchive> GetPipelineArchivesByReceiveLocation(string receiveLocation)
        {
            string lowerLocation = receiveLocation.ToLower();
            var query = db.PipelineArchives.Where(x => x.ReceiveLocation.ToLower() == lowerLocation);

            return query;
        }
    }
}

using ArchivePortal.Core;
using System;
using System.Collections.Generic;

namespace ArchivePortal.Data
{
    public interface IPipelineArchiveData
    {
        IEnumerable<PipelineArchive> GetPipelineArchivesByReceiveLocation(string receiveLocation);
        //PipelineArchive GetByInterchangeId(Guid interchangeId);
        int GetCountOfPipelineArchives();
    }
}

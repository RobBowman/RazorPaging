using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ArchivePortal.Core;

namespace ArchivePortal.Data
{
    public class InMemoryPipelineArchiveData : IPipelineArchiveData
    {
        readonly List<PipelineArchive> pipelineArchives;
        public InMemoryPipelineArchiveData()
        {
            pipelineArchives = new List<PipelineArchive>()
            {
                new PipelineArchive { InterchangeId = new Guid("7DD8829B-8D10-4173-9801-2D4931CFBA01"),
                CreatedOn = new DateTime(2019,01,14),
                ReceiveLocation = "RcvLocation1",
                ReceivedFilename = "RcvFilename1",
                },
                new PipelineArchive { InterchangeId = new Guid("7DD8829B-8D10-4173-9801-2D4931CFBA02"),
                CreatedOn = new DateTime(2019,01,15),
                ReceiveLocation = "RcvLocation2",
                ReceivedFilename = "RcvFilename2",
                }
            };
        }

        public int GetCountOfPipelineArchives()
        {
            return pipelineArchives.Count;
        }

        public IEnumerable<PipelineArchive> GetPipelineArchivesByReceiveLocation(string receiveLocation)
        {
            string lowerLocation = receiveLocation.ToLower();
            var query = pipelineArchives.Where(x => x.ReceiveLocation.ToLower() == lowerLocation);

            return query;
        }
    }
}

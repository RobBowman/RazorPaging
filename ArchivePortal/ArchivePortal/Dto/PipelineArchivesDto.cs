using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchivePortal.Dto
{
    public class PipelineArchiveDto
    {
        public Int64 PipelineArchivesId { get; set; }
        public string Property { get; set; }
        public Int64 Size { get; set; }
        public DateTime CreatedOn { get; set; }
        public double CreatedOnJulian { get; set; }
        public string CreatedOnString { get; set; }
        public Guid InterchangeId { get; set; }
        public string ReceiveLocation { get; set; }
        public string SendPort { get; set; }
        public string ReceivedFilename { get; set; }
        public string BodyIntro { get; set; }
        public string MsgDetail { get; set; }
    }
}

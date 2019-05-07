using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace ArchivePortal.Core
{
    [Table("tbl_PipelineArchives")]
    public class PipelineArchive
    {
        [Key]
        public Int64 PipelineArchivesId { get; set; }
        public Guid MessageId { get; set; }
        public Byte[] Body { get; set; }
        [MaxLength(200)]
        public string Property { get; set; }
        public Int64 Size { get; set; }
        public Boolean IsCompressed { get; set; }
        public Int64 CompressedSize { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid InterchangeId { get; set; }
        [MaxLength(50)]
        public string ReceiveLocation { get; set; }
        [MaxLength(50)]
        public string SendPort { get; set; }
        [MaxLength(50)]
        public string ReceivedFilename { get; set; }
        [MaxLength(100)]
        public string BodyIntro { get; set; }
    }
}

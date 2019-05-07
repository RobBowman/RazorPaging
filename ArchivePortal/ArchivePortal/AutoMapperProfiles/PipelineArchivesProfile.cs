using ArchivePortal.Core;
using ArchivePortal.Dto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchivePortal.AutoMapperProfiles
{
    public class PipelineArchivesProfile : Profile
    {
        public PipelineArchivesProfile()
        {
            CreateMap<PipelineArchive, PipelineArchiveDto>()
                .ForMember(q => q.CreatedOnJulian, option => option.Ignore())
                .ForMember(q => q.CreatedOnString, option => option.Ignore())
                .ForMember(q => q.MsgDetail, option => option.Ignore());
        }
    }
}

using ArchivePortal.Core;
using ArchivePortal.Data;
using ArchivePortal.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArchivePortal.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PipelineArchivesController : ControllerBase
    {
        private readonly PipelineArchiveDbContext _context;
        private readonly ILogger<PipelineArchivesController> _logger;
        private readonly IMapper _mapper;

        public PipelineArchivesController(PipelineArchiveDbContext context, ILogger<PipelineArchivesController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/PipelineArchives
        [HttpGet]
        public IEnumerable<PipelineArchiveDto> GetPipelineArchives()
        {
            _logger.LogTrace("Demo trace 1");
            string baseUrl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, Request.PathBase);

            List<PipelineArchiveDto> ret = new List<PipelineArchiveDto>();

            foreach(var rec in _context.PipelineArchives.Select(p => new PipelineArchive() {InterchangeId = p.InterchangeId,
                                                                        PipelineArchivesId = p.PipelineArchivesId,
                                                                        Property = p.Property,
                                                                        Size = p.Size,
                                                                        CreatedOn = p.CreatedOn,
                                                                        ReceiveLocation = p.ReceiveLocation,
                                                                        SendPort = p.SendPort,
                                                                        ReceivedFilename = p.ReceivedFilename,
                                                                        BodyIntro = p.BodyIntro
                                                                        } ))
            {
                PipelineArchiveDto dtoRec = _mapper.Map<PipelineArchive, PipelineArchiveDto>(rec);
                dtoRec.CreatedOnJulian = rec.CreatedOn.ToOADate() + 2415018.5;
                dtoRec.CreatedOnString = rec.CreatedOn.ToString("dd/MM/yy hh:mm:ss");
                dtoRec.BodyIntro = $@"<a href=""{baseUrl}/MessageDetail/{rec.PipelineArchivesId}"">{rec.BodyIntro}</a>";
                ret.Add(dtoRec);
            }
            return ret;
        }

        // GET: api/PipelineArchives/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPipelineArchive([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var PipelineArchive = await _context.PipelineArchives.FindAsync(id);

            if (PipelineArchive == null)
            {
                return NotFound();
            }

            return Ok(PipelineArchive);
        }


        private bool PipelineArchiveExists(Guid interchangeId)
        {
            return _context.PipelineArchives.Any(e => e.InterchangeId == interchangeId);
        }
    }
}

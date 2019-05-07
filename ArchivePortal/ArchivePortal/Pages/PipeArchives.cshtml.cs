using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchivePortal.Core;
using ArchivePortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ArchivePortal.Pages
{
    public class PipeArchivesModel : PageModel
    {
        private readonly PipelineArchiveDbContext _context;

        public PipeArchivesModel(PipelineArchiveDbContext context)
        {
            this._context = context;
        }

        public PaginatedList<PipelineArchive> PipeArcPagList { get; set; }

        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CreatedOnSort"] = String.IsNullOrEmpty(sortOrder) ? "createdOn_desc" : "";
            ViewData["RcvLocSort"] = sortOrder == "ReceiveLocation" ? "rcvLoc_desc" : "ReceiveLocation";
            ViewData["RcvFileSort"] = sortOrder == "ReceiveFilename" ? "rcvFile_desc" : "ReceiveFilename";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            IQueryable<PipelineArchive> pipelineRecs = _context.PipelineArchives.Select(p => new PipelineArchive()
                                            {
                                                InterchangeId = p.InterchangeId,
                                                PipelineArchivesId = p.PipelineArchivesId,
                                                Property = p.Property,
                                                Size = p.Size,
                                                CreatedOn = p.CreatedOn,
                                                ReceiveLocation = p.ReceiveLocation,
                                                SendPort = p.SendPort,
                                                ReceivedFilename = p.ReceivedFilename,
                                                BodyIntro = p.BodyIntro
                                            });
            if (!String.IsNullOrEmpty(searchString))
            {
                pipelineRecs = pipelineRecs.Where(s => s.ReceiveLocation.Contains(searchString)
                                       || s.ReceivedFilename.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "createdOn_desc":
                    pipelineRecs = pipelineRecs.OrderByDescending(s => s.CreatedOn);
                    break;
                case "ReceiveLocation":
                    pipelineRecs = pipelineRecs.OrderBy(s => s.ReceiveLocation);
                    break;
                case "rcvLoc_desc":
                    pipelineRecs = pipelineRecs.OrderByDescending(s => s.ReceiveLocation);
                    break;
                case "rcvFile_desc":
                    pipelineRecs = pipelineRecs.OrderByDescending(s => s.ReceivedFilename);
                    break;
                default:
                    pipelineRecs = pipelineRecs.OrderBy(s => s.CreatedOn);
                    break;
            }

            int pageSize = 3;
            PipeArcPagList = await PaginatedList<PipelineArchive>.CreateAsync(
                pipelineRecs.AsNoTracking(), pageIndex ?? 1, pageSize);
        }
    }
}
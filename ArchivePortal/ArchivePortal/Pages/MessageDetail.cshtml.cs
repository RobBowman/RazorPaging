using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArchivePortal.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace ArchivePortal.Pages
{
    public class MessageDetailModel : PageModel
    {
        private readonly PipelineArchiveDbContext _context;
        private readonly ILogger<MessageDetailModel> _logger;

        public string FullMsg { get; set; }

        public MessageDetailModel(PipelineArchiveDbContext context, ILogger<MessageDetailModel> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public IActionResult OnGet(Int64 pipelineArchivesId)
        {
            var binaryBody = _context.PipelineArchives.Find(pipelineArchivesId).Body;
            bool isXml;

            FullMsg = Helpers.MessageDetail.GetMsgString(_logger, binaryBody, out isXml);

            return Page();

        }
    }
}
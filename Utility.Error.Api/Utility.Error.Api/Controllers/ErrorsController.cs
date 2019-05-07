using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Utility.Error.Api.Extensions;
using Utility.Error.Application.Error.Models;

namespace Utility.Error.Api.Controllers
{
    /// <summary>
    /// Errors Controller.
    /// </summary>
    [Route("api/utility/errors")]
    [ApiController]
    public class ErrorsController : BaseController
    {
        // WebAPi Methods.
        #region WebAPIMethods

        /// <summary>
        /// GET.
        /// </summary>
        /// <param name="identifier"></param>
        [HttpGet("")]
        public async Task<IActionResult> Get([RequiredFromQuery] string identifier)
        {
            return Ok
            (
                await Mediator.Send
                (
                    new Application.Error.Queries.ByIdentifier.Query()
                    {
                        Identifier = identifier
                    }
                )
            );
        }

        /// <summary>
        /// PUT.
        /// </summary>
        /// <param name="request"></param>
        [HttpPut("")]
        public async Task<IActionResult> Put(ErrorDetailModel request)
        {
            await Mediator.Publish
            (
                new Application.Error.Commands.ErrorCommands.ErrorLog()
                {
                    Message = request
                }
            );
            return Ok();
        }
        
        #endregion
    }
}
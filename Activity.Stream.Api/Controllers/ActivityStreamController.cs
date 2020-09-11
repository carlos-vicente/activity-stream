using System;
using System.Threading.Tasks;
using Activity.Stream.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Activity.Stream.Api.Controllers
{
    /// <summary>
    /// All actions related to activity streams and corresponding tags
    /// </summary>
    [ApiController]
    [Route("/activity-stream")]
    public class ActivityStreamController : Controller
    {
        private readonly IMediator _mediator;

        public ActivityStreamController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        /// <summary>
        /// Get the specified activity stream
        /// </summary>
        /// <remarks>
        /// Get the activity stream identified in the request. All activity entries will be returned.
        /// Optionally start and/or end date filters can be specified to obtain only a slice of the activity entries.
        /// </remarks>
        /// <param name="id">The stream's identifier</param>
        /// <param name="startDate">An optional date to filter stream log from</param>
        /// <param name="endDate">An optional date to filter stream log until</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetActivityStream(
            [FromRoute] string id,
            [FromQuery] DateTime? startDate = null,
            [FromQuery] DateTime? endDate = null)
        {
            var request = new GetActivityStreamRequest
            {
                Id = id,
                StartDate = startDate,
                EndDate = endDate
            };

            var response = await _mediator
                .Send(request)
                .ConfigureAwait(false);
            
            return Json(response);
        }
    }
}
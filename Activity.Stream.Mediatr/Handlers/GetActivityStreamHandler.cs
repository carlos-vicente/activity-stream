using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Activity.Stream.Contracts;
using Activity.Stream.Contracts.Data;
using MediatR;

namespace Activity.Stream.Mediatr.Handlers
{
    public class GetActivityStreamHandler : IRequestHandler<GetActivityStreamRequest, GetActivityStreamResponse>
    {
        public Task<GetActivityStreamResponse> Handle(
            GetActivityStreamRequest request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new GetActivityStreamResponse
            {
                Id = request.Id,
                Entries = new List<ActivityEntry>()
            });
        }
    }
}
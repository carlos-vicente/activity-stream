using System;
using MediatR;

namespace Activity.Stream.Contracts
{
    public class GetActivityStreamRequest : IRequest<GetActivityStreamResponse>
    {
        public string Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
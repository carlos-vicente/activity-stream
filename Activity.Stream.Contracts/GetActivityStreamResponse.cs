using System.Collections.Generic;
using Activity.Stream.Contracts.Data;

namespace Activity.Stream.Contracts
{
    public class GetActivityStreamResponse
    {
        public string Id { get; set; }
        public IEnumerable<ActivityEntry> Entries { get; set; }
    }
}
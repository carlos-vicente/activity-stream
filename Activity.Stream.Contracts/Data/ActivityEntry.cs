using System;

namespace Activity.Stream.Contracts.Data
{
    public class ActivityEntry
    {
        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
    }
}
using Jane.Entities;
using MongoDB.Bson;
using System;

namespace JaneGenericHostExample.Store
{
    public class ContentPublishedVersion : Entity<ObjectId>
    {
        public string AggregateRootId { get; set; }

        public string AggregateRootTypeName { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProcessorName { get; set; }

        public int Version { get; set; }
    }
}
using Jane.Entities;
using MongoDB.Bson;
using System;

namespace JaneGenericHostExample.Store
{
    public class ContentEventStream : Entity<ObjectId>
    {
        public string AggregateRootId { get; set; }

        public string AggregateRootTypeName { get; set; }

        public string CommandId { get; set; }

        public DateTime CreatedOn { get; set; }

        public string Events { get; set; }

        public int Version { get; set; }
    }
}
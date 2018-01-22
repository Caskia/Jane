using System;

namespace Jane.Entities.Auditing
{
    [Serializable]
    public abstract class FullAuditedVersionEntity : FullAuditedVersionEntity<int>
    {
    }

    [Serializable]
    public abstract class FullAuditedVersionEntity<TPrimaryKey> : FullAuditedEntity<TPrimaryKey>, IHasVersion
    {
        public int EventSequence { get; set; }

        public int Version { get; set; }
    }
}
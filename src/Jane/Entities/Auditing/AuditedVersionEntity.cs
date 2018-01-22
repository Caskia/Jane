using System;

namespace Jane.Entities.Auditing
{
    [Serializable]
    public abstract class AuditedVersionEntity : AuditedVersionEntity<int>
    {
    }

    [Serializable]
    public abstract class AuditedVersionEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IHasVersion
    {
        public int EventSequence { get; set; }

        public int Version { get; set; }
    }
}
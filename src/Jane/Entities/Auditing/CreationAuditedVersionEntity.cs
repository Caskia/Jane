using System;

namespace Jane.Entities.Auditing
{
    [Serializable]
    public abstract class CreationAuditedVersionEntity : CreationAuditedVersionEntity<int>
    {
    }

    [Serializable]
    public abstract class CreationAuditedVersionEntity<TPrimaryKey> : CreationAuditedEntity<TPrimaryKey>, IHasVersion
    {
        public int EventSequence { get; set; }

        public int Version { get; set; }
    }
}
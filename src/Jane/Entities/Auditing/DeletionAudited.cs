using System;

namespace Jane.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IDeletionAudited"/>.
    /// </summary>
    [Serializable]
    public class DeletionAudited : IDeletionAudited
    {
        /// <summary>
        /// Which user deleted this entity?
        /// </summary>
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// Deletion time of this entity.
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// Is this entity Deleted?
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
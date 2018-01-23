using System;

namespace Jane.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IFullAudited"/>.
    /// </summary>
    [Serializable]
    public class FullAudited : Audited, IFullAudited
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
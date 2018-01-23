using Jane.Timing;
using System;

namespace Jane.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="ICreationAudited"/>.
    /// </summary>
    [Serializable]
    public class CreationAudited : ICreationAudited
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public CreationAudited()
        {
            CreationTime = Clock.Now;
        }

        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// Creator of this entity.
        /// </summary>
        public virtual long? CreatorUserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Jane.Entities.Auditing
{
    /// <summary>
    /// This class can be used to simplify implementing <see cref="IModificationAudited"/>.
    /// </summary>
    [Serializable]
    public class ModificationAudited : IModificationAudited
    {
        /// <summary>
        /// Last modification date of this entity.
        /// </summary>
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// Last modifier user of this entity.
        /// </summary>
        public virtual long? LastModifierUserId { get; set; }
    }
}
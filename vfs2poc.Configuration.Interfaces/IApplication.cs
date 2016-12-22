using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IApplication : IConfigObject
    {
        IList<ICulture> Cultures { get; set; }

        /// <summary>
        /// Global fields, not attached to entity types
        /// </summary>
        IList<IField> GlobalFields { get; set; }

        IList<IEntityType> EntityTypes { get; set; }
    }
}

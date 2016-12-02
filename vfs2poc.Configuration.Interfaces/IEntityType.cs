using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IEntityType : IModelObject
    {
        IApplication Application { get; set; }

        IList<IField> Fields { get; set; }
    }
}

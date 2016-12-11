using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IEntityType : IModelObject
    {
        IApplication Application { get; set; }

        string Code { get; set; }

        IList<IField> Fields { get; set; }

        IEntityType Parent { get; set; }

        IList<IEntityType> Children { get; set; }
    }
}

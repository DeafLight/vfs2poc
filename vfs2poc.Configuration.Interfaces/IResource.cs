using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IResource : IModelObject
    {
        IApplication Application { get; set; }

        IList<IResourceValue> ResourceValues { get; set; }
    }
}

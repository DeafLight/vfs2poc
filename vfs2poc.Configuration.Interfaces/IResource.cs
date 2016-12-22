using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IResource : IConfigObject
    {
        IApplication Application { get; set; }

        IList<IResourceValue> ResourceValues { get; set; }
    }
}

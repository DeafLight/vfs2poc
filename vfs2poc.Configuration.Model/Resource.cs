using System;
using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class Resource : ModelObject, IResource
    {
        public Resource()
        {
            ResourceValues = new List<IResourceValue>();
        }

        public IApplication Application { get; set; }

        public IList<IResourceValue> ResourceValues { get; set; }
    }
}

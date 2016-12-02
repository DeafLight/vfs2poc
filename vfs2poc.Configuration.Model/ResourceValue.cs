using System;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class ResourceValue : ModelObject, IResourceValue
    {
        public ICulture Culture { get; set; }

        public IResource Resource { get; set; }

        public string Value { get; set; }
    }
}

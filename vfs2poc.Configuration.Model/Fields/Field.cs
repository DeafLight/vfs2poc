using System;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public abstract class Field : ModelObject, IField
    {
        public string Code { get; set; }

        public IResource Name { get; set; }

        public IEntityType EntityType { get; set; }
    }
}

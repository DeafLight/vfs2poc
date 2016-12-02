using System;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class Field : ModelObject, IField
    {
        public IResource Name { get; set; }
    }
}

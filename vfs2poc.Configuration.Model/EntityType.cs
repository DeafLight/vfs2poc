using System;
using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class EntityType : ConfigObject, IEntityType
    {
        public EntityType()
        {
            Fields = new List<IField>();
            Children = new List<IEntityType>();
        }

        public IApplication Application { get; set; }

        public string Code { get; set; }

        public IList<IField> Fields { get; set; }

        public IEntityType Parent { get; set; }

        public IList<IEntityType> Children { get; set; }

    }
}

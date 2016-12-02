using System;
using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class Application : ModelObject, IApplication
    {
        public Application()
        {
            Cultures = new List<ICulture>();
            Fields = new List<IField>();
            EntityTypes = new List<IEntityType>();
        }

        public IList<ICulture> Cultures { get; set; }

        public IList<IField> Fields { get; set; }

        public IList<IEntityType> EntityTypes { get; set; }
    }
}

using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationType : ModelObject, IRelationType
    {
        public RelationType()
        {
            EntityTypes = new Dictionary<string, IEntityType>();
            Vertices = new List<IRelationVertex>();
        }

        public IApplication Application { get; set; }

        public string Code { get; set; }

        public IDictionary<string, IEntityType> EntityTypes { get; set; }

        public IList<IRelationVertex> Vertices { get; set; }
    }
}

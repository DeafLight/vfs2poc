using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationVertex : IRelationVertex
    {
        public KeyValuePair<string, IEntityType> NodeLeft { get; set; }

        public KeyValuePair<string, IEntityType> NodeRight { get; set; }

        public int? LeftMin { get; set; }

        public int? LeftMax { get; set; }

        public int? RightMin { get; set; }

        public int? RightMax { get; set; }
    }
}

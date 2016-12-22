using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationView : View, IRelationView
    {
        public IRelationType RelationType { get; set; }
    }
}

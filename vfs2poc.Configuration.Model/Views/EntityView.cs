using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class EntityView : View, IEntityView
    {
        public IEntityType EntityType { get; set; }
    }
}

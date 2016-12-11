namespace vfs2poc.Configuration.Interfaces
{
    public interface IEntityView : IView
    {
        IEntityType EntityType { get; set; }
    }
}

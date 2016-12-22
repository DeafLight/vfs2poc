namespace vfs2poc.Configuration.Interfaces
{
    public interface IRelationView : IView
    {
        IRelationType RelationType { get; set; }
    }
}

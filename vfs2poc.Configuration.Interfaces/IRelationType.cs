namespace vfs2poc.Configuration.Interfaces
{
    public interface IRelationType : IModelObject
    {
        IApplication Application { get; set; }

        string Code { get; set; }
    }
}

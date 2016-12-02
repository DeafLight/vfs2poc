namespace vfs2poc.Configuration.Interfaces
{
    public interface ICulture : IModelObject
    {
        IApplication Application { get; set; }

        string Code { get; set; }
    }
}

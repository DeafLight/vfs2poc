namespace vfs2poc.Configuration.Interfaces
{
    public interface IResourceValue : IModelObject
    {
        string Value { get; set; }

        ICulture Culture { get; set; }

        IResource Resource { get; set; }
    }
}

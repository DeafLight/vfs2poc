namespace vfs2poc.Configuration.Interfaces
{
    public interface IResourceValue : IConfigObject
    {
        string Value { get; set; }

        ICulture Culture { get; set; }

        IResource Resource { get; set; }
    }
}

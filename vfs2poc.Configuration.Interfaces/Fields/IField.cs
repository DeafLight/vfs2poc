namespace vfs2poc.Configuration.Interfaces
{
    public interface IField : IModelObject
    {
        string Code { get; set; }

        IResource Name { get; set; }
    }
}

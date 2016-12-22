namespace vfs2poc.Configuration.Interfaces
{
    public interface IField : IConfigObject
    {
        string Code { get; set; }

        IResource Name { get; set; }

        IEntityType EntityType { get; set; }
    }
}

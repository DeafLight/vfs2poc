namespace vfs2poc.Configuration.Interfaces
{
    public interface ICulture : IConfigObject
    {
        IApplication Application { get; set; }

        string Code { get; set; }
    }
}

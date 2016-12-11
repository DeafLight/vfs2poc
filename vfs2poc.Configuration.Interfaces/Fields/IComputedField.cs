namespace vfs2poc.Configuration.Interfaces
{
    public interface IComputedField : IField
    {
        string Query { get; set; }
    }
}

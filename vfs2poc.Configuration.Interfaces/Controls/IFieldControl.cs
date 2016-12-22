namespace vfs2poc.Configuration.Interfaces
{
    public interface IFieldControl : IControl
    {
        string Discriminant { get; set; }

        IField Field { get; set; }
    }
}

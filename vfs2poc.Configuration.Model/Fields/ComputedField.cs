using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class ComputedField : Field, IComputedField
    {
        public string Query { get; set; }
    }
}

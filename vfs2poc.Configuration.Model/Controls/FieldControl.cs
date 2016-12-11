using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class FieldControl : Control, IFieldControl
    {
        public IField Field { get; set; }
    }
}

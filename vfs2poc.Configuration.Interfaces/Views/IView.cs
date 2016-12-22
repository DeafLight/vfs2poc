using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IView : IConfigObject
    {
        IList<IControl> Controls { get; set; }
    }
}

using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IView : IModelObject
    {
        IList<IControl> Controls { get; set; }
    }
}

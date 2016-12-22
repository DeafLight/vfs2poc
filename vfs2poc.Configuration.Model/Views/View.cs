using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public abstract class View : ConfigObject, IView
    {
        public View()
        {
            Controls = new List<IControl>();
        }

        public IList<IControl> Controls { get; set; }
    }
}

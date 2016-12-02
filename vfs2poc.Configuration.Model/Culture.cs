using System;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class Culture : ModelObject, ICulture
    {
        public IApplication Application { get; set; }

        public string Code { get; set; }
    }
}

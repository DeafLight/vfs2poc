using System;
using System.Collections.Generic;
using System.Linq;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    [Serializable]
    public class Resource : ConfigObject, IResource
    {
        public Resource()
        {
            ResourceValues = new List<IResourceValue>();
        }

        public Resource(IApplication app, IDictionary<string, string> values)
        {
            ResourceValues = new List<IResourceValue>(values.Where(x => app.Cultures.Any(y => y.Code == x.Key)).Select(x => new ResourceValue
            {
                Culture = app.Cultures.First(y => y.Code == x.Key),
                Id = Guid.NewGuid(),
                IsFixed = false,
                Resource = this,
                Value = x.Value,
            }));
        }

        public IApplication Application { get; set; }

        public IList<IResourceValue> ResourceValues { get; set; }
    }
}

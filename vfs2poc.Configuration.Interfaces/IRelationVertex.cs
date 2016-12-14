using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public interface IRelationVertex
    {
        KeyValuePair<string, IEntityType> NodeLeft { get; set; }

        KeyValuePair<string, IEntityType> NodeRight { get; set; }

        int? LeftMin { get; set; }

        int? LeftMax { get; set; }

        int? RightMin { get; set; }

        int? RightMax { get; set; }
    }
}

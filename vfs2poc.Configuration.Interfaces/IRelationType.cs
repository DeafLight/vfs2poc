using System.Collections.Generic;
using vfs2poc.Configuration.Model;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IRelationType : IModelObject
    {
        IApplication Application { get; set; }

        string Code { get; set; }

        IDictionary<string, IEntityType> EntityTypes { get; set; }

        IList<IRelationVertex> Vertices { get; set; }

        void AddEntityType(IEntityType entityType, string alias);

        bool RemoveEntityType(string alias);

        IEntityType GetEntityType(string alias);

        void AddVertex(string aliasLeft, string aliasRight, int? leftMin, int? leftMax, int? rightMin, int? rightMax);

        bool RemoveVertex(string aliasLeft, string aliasRight);

        IRelationVertex GetVertex(string aliasLeft, string aliasRight);
    }
}

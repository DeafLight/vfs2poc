using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IRelationType : IModelObject
    {
        IApplication Application { get; set; }

        string Code { get; set; }

        IDictionary<string, IRelationNode> Nodes { get; set; }

        void AddNode(IModelObject modelObject, string alias);

        bool RemoveNode(string alias);

        bool RemoveNode(IRelationNode node);

        IRelationNode GetNode(string alias);

        void AddVertex(string leftAlias, string rightAlias, ICardinality leftCardinality, ICardinality rightCardinality);

        void AddVertex(IRelationNode leftNode, IRelationNode rightNode, ICardinality leftCardinality, ICardinality rightCardinality);

        bool RemoveVertex(string leftAlias, string rightAlias);

        bool RemoveVertex(IRelationNode leftNode, IRelationNode rightNode);

        IRelationVertex GetVertex(string leftAlias, string rightAlias);

        IRelationVertex GetVertex(IRelationNode leftNode, IRelationNode rightNode);
    }
}

using System;
using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationType : ConfigObject, IRelationType
    {
        public RelationType()
        {
            Nodes = new Dictionary<string, IRelationNode>();
        }

        public RelationType(string code) : this()
        {
            Code = code;
        }

        public IApplication Application { get; set; }

        public string Code { get; set; }

        public IDictionary<string, IRelationNode> Nodes { get; set; }

        public void AddNode(IModelObject modelObject, string discriminant)
        {
            if (modelObject == null)
            {
                throw new ArgumentNullException(nameof(modelObject));
            }

            if (Nodes.ContainsKey(discriminant))
            {
                throw new ArgumentException(nameof(discriminant));
            }

            Nodes.Add(discriminant, new RelationNode(modelObject, discriminant, this));
        }

        public void AddVertex(IRelationNode leftNode, IRelationNode rightNode, ICardinality leftCardinality = null, ICardinality rightCardinality = null)
        {
            if (leftNode == null)
            {
                throw new ArgumentNullException(nameof(leftNode));
            }

            if (rightNode == null)
            {
                throw new ArgumentNullException(nameof(rightNode));
            }

            if (leftNode.Vertices.ContainsKey(rightNode.Discriminant) || rightNode.Vertices.ContainsKey(leftNode.Discriminant))
            {
                throw new ArgumentException(nameof(leftNode.Discriminant));
            }

            var vertex = new RelationVertex(leftNode, rightNode, leftCardinality, rightCardinality);

            leftNode.Vertices.Add(rightNode.Discriminant, vertex);
            rightNode.Vertices.Add(leftNode.Discriminant, vertex);
        }

        public void AddVertex(string leftDiscriminant, string rightDiscriminant, ICardinality leftCardinality = null, ICardinality rightCardinality = null)
        {
            AddVertex(Nodes[leftDiscriminant], Nodes[rightDiscriminant], leftCardinality, rightCardinality);
        }

        public IRelationNode GetNode(string discriminant)
        {
            if (!Nodes.ContainsKey(discriminant))
            {
                return null;
            }

            return Nodes[discriminant];
        }

        public IRelationVertex GetVertex(IRelationNode leftNode, IRelationNode rightNode)
        {
            if (leftNode == null)
            {
                throw new ArgumentNullException(nameof(leftNode));
            }

            if (rightNode == null)
            {
                throw new ArgumentNullException(nameof(rightNode));
            }

            return leftNode.GetVertex(rightNode) ?? rightNode.GetVertex(leftNode);
        }

        public IRelationVertex GetVertex(string leftDiscriminant, string rightDiscriminant)
        {
            return GetNode(leftDiscriminant)?.GetVertex(rightDiscriminant) ?? GetNode(rightDiscriminant)?.GetVertex(leftDiscriminant);
        }

        public bool RemoveNode(IRelationNode node)
        {
            if (node == null)
            {
                throw new ArgumentNullException(nameof(node));
            }

            return RemoveNode(node.Discriminant);
        }

        public bool RemoveNode(string discriminant)
        {
            return Nodes.Remove(discriminant);
        }

        public bool RemoveVertex(IRelationNode leftNode, IRelationNode rightNode)
        {
            if (leftNode == null)
            {
                throw new ArgumentNullException(nameof(leftNode));
            }

            if (rightNode == null)
            {
                throw new ArgumentNullException(nameof(rightNode));
            }

            return leftNode.Vertices.Remove(rightNode.Discriminant) & rightNode.Vertices.Remove(leftNode.Discriminant);
        }

        public bool RemoveVertex(string leftDiscriminant, string rightDiscriminant)
        {
            return (GetNode(leftDiscriminant)?.Vertices.Remove(rightDiscriminant)).GetValueOrDefault() & (GetNode(rightDiscriminant)?.Vertices.Remove(leftDiscriminant)).GetValueOrDefault();
        }
    }
}

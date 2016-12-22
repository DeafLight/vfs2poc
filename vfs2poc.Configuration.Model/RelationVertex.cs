using System;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationVertex : IRelationVertex
    {
        public RelationVertex(IRelationNode leftNode, IRelationNode rightNode, ICardinality leftCardinality = null, ICardinality rightCardinality = null)
        {
            if (leftNode == null)
            {
                throw new ArgumentNullException(nameof(leftNode));
            }

            if (rightNode == null)
            {
                throw new ArgumentNullException(nameof(rightNode));
            }

            if (leftNode == RightNode || leftNode.Discriminant == rightNode.Discriminant)
            {
                throw new ArgumentException("discriminants should be different");
            }

            LeftNode = leftNode;
            RightNode = rightNode;
            LeftCardinality = leftCardinality ?? new Cardinality();
            RightCardinality = leftCardinality ?? new Cardinality();
        }

        public IRelationNode LeftNode { get; set; }

        public IRelationNode RightNode { get; set; }

        public ICardinality LeftCardinality { get; set; }

        public ICardinality RightCardinality { get; set; }
    }
}

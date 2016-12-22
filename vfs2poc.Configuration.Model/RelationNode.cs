using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationNode : ConfigObject, IRelationNode
    {
        public RelationNode(IModelObject modelObject, string discriminant, IRelationType container = null)
        {
            Vertices = new Dictionary<string, IRelationVertex>();
            ModelObject = modelObject;
            Discriminant = discriminant;
            Container = container;
        }

        public IRelationType Container { get; set; }

        public string Discriminant { get; set; }

        public IModelObject ModelObject { get; set; }

        public IDictionary<string, IRelationVertex> Vertices { get; set; }

        public IRelationVertex GetVertex(IRelationNode oppositeNode)
        {
            return GetVertex(oppositeNode.Discriminant);
        }

        public IRelationVertex GetVertex(string oppositeDiscriminant)
        {
            if (!Vertices.ContainsKey(oppositeDiscriminant))
            {
                return null;
            }

            return Vertices[oppositeDiscriminant];
        }
    }
}

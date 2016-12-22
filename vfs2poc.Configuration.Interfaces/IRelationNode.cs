using System.Collections.Generic;

namespace vfs2poc.Configuration.Interfaces
{
    public interface IRelationNode : IConfigObject
    {
        IRelationType Container { get; set; }

        IModelObject ModelObject { get; set; }

        string Discriminant { get; set; }

        IDictionary<string, IRelationVertex> Vertices { get; set; }

        IRelationVertex GetVertex(string oppositeDiscriminant);

        IRelationVertex GetVertex(IRelationNode oppositeNode);
    }
}

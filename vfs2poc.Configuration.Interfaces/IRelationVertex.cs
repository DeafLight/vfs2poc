namespace vfs2poc.Configuration.Interfaces
{
    public interface IRelationVertex
    {
        IRelationNode LeftNode { get; set; }

        IRelationNode RightNode { get; set; }

        ICardinality LeftCardinality { get; set; }

        ICardinality RightCardinality { get; set; }
    }
}

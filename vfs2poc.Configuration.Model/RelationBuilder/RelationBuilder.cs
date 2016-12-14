using System.Collections.Generic;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model.RelationBuilder
{
    public class RelationBuilder
    {
        public static RelationBuilder NewRelation(string code)
        {
            return new RelationBuilder(code);
        }

        protected RelationType RelationType { get; set; }

        public RelationBuilder(string code)
        {
            RelationType = new RelationType
            {
                Code = code,
            };
        }

        public EntityTypePart AddNode(IEntityType entityType, string alias)
        {
            var entityTypePart = new EntityTypePart
            {
                RelationBuilder = this,
                EntityType = new KeyValuePair<string, IEntityType>(alias, entityType),
            };

            RelationType.EntityTypes.Add(entityTypePart.EntityType);

            return entityTypePart;
        }

        public RelationVertexPart AddVertex(string aliasLeft, string aliasRight, int? leftMin, int? leftMax, int? rightMin, int? rightMax)
        {
            var relationVertexPart = new RelationVertexPart
            {
                RelationBuilder = this,
                RelationVertex = new RelationVertex
                {
                    NodeLeft = new KeyValuePair<string, IEntityType>(aliasLeft, RelationType.EntityTypes[aliasLeft]),
                    NodeRight = new KeyValuePair<string, IEntityType>(aliasRight, RelationType.EntityTypes[aliasRight]),
                    LeftMin = leftMin,
                    LeftMax = leftMax,
                    RightMin = rightMin,
                    RightMax = rightMax,
                }
            };

            // TODO: add cardinality

            RelationType.Vertices.Add(relationVertexPart.RelationVertex);

            return relationVertexPart;
        }

        public RelationType Build()
        {
            return RelationType;
        }
    }

    public class EntityTypePart
    {
        public RelationBuilder RelationBuilder { get; set; }

        public KeyValuePair<string, IEntityType> EntityType { get; set; }

        public EntityTypePart JoinsTo(EntityType entityType, string alias, int? leftMin, int? leftMax, int? rightMin, int? rightMax)
        {
            var entityTypePart = new EntityTypePart
            {
                RelationBuilder = RelationBuilder,
                EntityType = new KeyValuePair<string, IEntityType>(alias, entityType),
            };

            var node = RelationBuilder.AddNode(entityType, alias);
            RelationBuilder.AddVertex(EntityType.Key, alias, leftMin, leftMax, rightMin, rightMax);

            return node;
        }

        public RelationType Build()
        {
            return RelationBuilder.Build();
        }
    }

    public class RelationVertexPart
    {
        public RelationBuilder RelationBuilder { get; set; }

        public IRelationVertex RelationVertex { get; set; }

        public RelationType Build()
        {
            return RelationBuilder.Build();
        }
    }
}

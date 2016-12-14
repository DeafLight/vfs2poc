using System;
using System.Collections.Generic;
using System.Linq;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model
{
    public class RelationType : ModelObject, IRelationType
    {
        public RelationType()
        {
            EntityTypes = new Dictionary<string, IEntityType>();
            Vertices = new List<IRelationVertex>();
        }

        public IApplication Application { get; set; }

        public string Code { get; set; }

        public IDictionary<string, IEntityType> EntityTypes { get; set; }

        public IList<IRelationVertex> Vertices { get; set; }

        /// <summary>
        /// Adds or replace an entitytype for a given alias
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="alias"></param>
        public void AddEntityType(IEntityType entityType, string alias)
        {
            if (alias == null)
            {
                throw new ArgumentNullException(nameof(alias));
            }

            if (entityType == null)
            {
                throw new ArgumentNullException(nameof(entityType));
            }

            if (!EntityTypes.ContainsKey(alias))
            {
                EntityTypes.Add(alias, entityType);
            }
        }

        public void AddVertex(string aliasLeft, string aliasRight, int? leftMin, int? leftMax, int? rightMin, int? rightMax)
        {
            if (leftMin.GetValueOrDefault() > leftMax.GetValueOrDefault(int.MaxValue))
            {
                throw new ArgumentException($"{nameof(leftMin)} should be less than {nameof(leftMax)}");
            }

            if (rightMin.GetValueOrDefault() > rightMax.GetValueOrDefault(int.MaxValue))
            {
                throw new ArgumentException($"{nameof(rightMin)} should be less than {nameof(rightMax)}");
            }

            IEntityType nodeLeft, nodeRight;
            EntityTypes.TryGetValue(aliasLeft, out nodeLeft);

            if (nodeLeft == null)
            {
                throw new ArgumentException(nameof(aliasLeft));
            }

            EntityTypes.TryGetValue(aliasRight, out nodeRight);

            if (nodeRight == null)
            {
                throw new ArgumentException(nameof(aliasRight));
            }

            if (!Vertices.Any(x => (x.NodeLeft.Key == aliasLeft && x.NodeRight.Key == aliasRight)
                || (x.NodeLeft.Key == aliasRight && x.NodeRight.Key == aliasLeft)))
            {
                Vertices.Add(new RelationVertex
                {
                    NodeLeft = new KeyValuePair<string, IEntityType>(aliasLeft, nodeLeft),
                    NodeRight = new KeyValuePair<string, IEntityType>(aliasRight, nodeRight),
                    LeftMin = leftMin,
                    LeftMax = leftMax,
                    RightMin = rightMin,
                    RightMax = rightMax,
                });
            }
        }

        public IEntityType GetEntityType(string alias)
        {
            IEntityType entityType;
            EntityTypes.TryGetValue(alias, out entityType);

            return entityType;
        }

        public IRelationVertex GetVertex(string aliasLeft, string aliasRight)
        {
            return Vertices.FirstOrDefault(x => (x.NodeLeft.Key == aliasLeft && x.NodeRight.Key == aliasRight)
                || (x.NodeLeft.Key == aliasRight && x.NodeRight.Key == aliasLeft));
        }

        public bool RemoveEntityType(string alias)
        {
            return EntityTypes.Remove(alias);
        }

        public bool RemoveVertex(string aliasLeft, string aliasRight)
        {
            return Vertices.Remove(Vertices.FirstOrDefault(x => (x.NodeLeft.Key == aliasLeft && x.NodeRight.Key == aliasRight)
                || (x.NodeLeft.Key == aliasRight && x.NodeRight.Key == aliasLeft)));
        }
    }
}

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vfs2poc.Configuration.Interfaces;

namespace vfs2poc.Configuration.Model.Tests
{
    [TestClass]
    public class RelationTypeTest
    {
        [TestMethod]
        public void AddEntityType()
        {
            var a = new EntityType
            {
                Code = "A",
            };

            var relation = new RelationType();

            relation.AddNode(a, "A");

            Assert.AreEqual(relation.Nodes.Count, 1);
            Assert.AreEqual(relation.GetNode("A").ModelObject, a);

            Assert.AreEqual(relation.Nodes.Values.SelectMany(x => x.Vertices).Select(x => x.Value).Count(), 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullEntityType()
        {
            var relation = new RelationType();

            relation.AddNode(null, "A");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullAlias()
        {
            var a = new EntityType
            {
                Code = "A",
            };

            var relation = new RelationType();

            relation.AddNode(a, null);
        }

        /// <summary>
        /// [A]1..1-1..1[B]
        /// </summary>
        [TestMethod]
        public void OneToOneRelation()
        {
            var a = new EntityType
            {
                Code = "A",
            };

            var b = new EntityType
            {
                Code = "B",
            };

            var relation = new RelationType("rel1");
            relation.AddNode(a, "A");
            relation.AddNode(b, "B");
            relation.AddVertex("A", "B", new Cardinality(1, 1), new Cardinality(1, 1));

            Assert.AreEqual(relation.Nodes.Count, 2);
            Assert.AreEqual(relation.GetNode("A").ModelObject, a);
            Assert.AreEqual(relation.GetNode("B").ModelObject, b);

            Assert.AreEqual(relation.Nodes.Values.SelectMany(x => x.Vertices).Select(x => x.Value).Distinct().Count(), 1);

            var vertex = relation.GetVertex("A", "B");
            Assert.AreEqual(vertex.LeftCardinality.Min, 1);
            Assert.AreEqual(vertex.LeftCardinality.Max, 1);
            Assert.AreEqual(vertex.RightCardinality.Min, 1);
            Assert.AreEqual(vertex.RightCardinality.Max, 1);
        }

        /// <summary>
        /// [A]0..M-0..N[B]
        /// </summary>
        [TestMethod]
        public void MToNRelation()
        {
            var a = new EntityType
            {
                Code = "A",
            };

            var b = new EntityType
            {
                Code = "B",
            };

            var relation = new RelationType("rel1");
            relation.AddNode(a, "A");
            relation.AddNode(b, "B");
            relation.AddVertex("A", "B");

            Assert.AreEqual(relation.Nodes.Count, 2);
            Assert.AreEqual(relation.GetNode("A").ModelObject, a);
            Assert.AreEqual(relation.GetNode("B").ModelObject, b);

            Assert.AreEqual(relation.Nodes.Values.SelectMany(x => x.Vertices).Select(x => x.Value).Distinct().Count(), 1);

            var vertex = relation.GetVertex("A", "B");
            Assert.AreEqual(vertex.LeftCardinality.Min, 0);
            Assert.AreEqual(vertex.LeftCardinality.Max, int.MaxValue);
            Assert.AreEqual(vertex.RightCardinality.Min, 0);
            Assert.AreEqual(vertex.RightCardinality.Max, int.MaxValue);
        }

        /// <summary>
        /// [A]0..M-0..N[B]0..P-0..Q[C]
        /// </summary>
        [TestMethod]
        public void IndirectRelation()
        {
            var a = new EntityType
            {
                Code = "A",
            };

            var b = new EntityType
            {
                Code = "B",
            };

            var c = new EntityType
            {
                Code = "C",
            };

            var relation = new RelationType("rel1");
            relation.AddNode(a, "A");
            relation.AddNode(b, "B");
            relation.AddNode(c, "C");
            relation.AddVertex("A", "B");
            relation.AddVertex("B", "C");

            Assert.AreEqual(relation.Nodes.Count, 3);
            Assert.AreEqual(relation.GetNode("A").ModelObject, a);
            Assert.AreEqual(relation.GetNode("B").ModelObject, b);
            Assert.AreEqual(relation.GetNode("C").ModelObject, c);

            Assert.AreEqual(relation.Nodes.Values.SelectMany(x => x.Vertices).Select(x => x.Value).Distinct().Count(), 2);

            var abVertex = relation.GetVertex("A", "B");
            Assert.AreEqual(abVertex.LeftNode.Discriminant, "A");
            Assert.AreEqual(abVertex.RightNode.Discriminant, "B");

            var bcVertex = relation.GetVertex("B", "C");
            Assert.AreEqual(bcVertex.LeftNode.Discriminant, "B");
            Assert.AreEqual(bcVertex.RightNode.Discriminant, "C");

            Assert.IsNull(relation.GetVertex("A", "C"));
        }

        /// <summary>
        /// [A]0..M-0..N[B]
        /// [A]0..P-0..Q[C]
        /// </summary>
        [TestMethod]
        public void TernaryRelation()
        {
            var a = new EntityType
            {
                Code = "A",
            };

            var b = new EntityType
            {
                Code = "B",
            };

            var c = new EntityType
            {
                Code = "C",
            };

            var relation = new RelationType("rel1");
            relation.AddNode(a, "A");
            relation.AddNode(b, "B");
            relation.AddNode(c, "C");
            relation.AddVertex("A", "B");
            relation.AddVertex("A", "C");

            Assert.AreEqual(relation.Nodes.Count, 3);
            Assert.AreEqual(relation.GetNode("A").ModelObject, a);
            Assert.AreEqual(relation.GetNode("B").ModelObject, b);
            Assert.AreEqual(relation.GetNode("C").ModelObject, c);

            Assert.AreEqual(relation.Nodes.Values.SelectMany(x => x.Vertices).Select(x => x.Value).Distinct().Count(), 2);

            var abVertex = relation.GetVertex("A", "B");
            Assert.AreEqual(abVertex.LeftNode.Discriminant, "A");
            Assert.AreEqual(abVertex.RightNode.Discriminant, "B");

            var acVertex = relation.GetVertex("A", "C");
            Assert.AreEqual(acVertex.LeftNode.Discriminant, "A");
            Assert.AreEqual(acVertex.RightNode.Discriminant, "C");

            Assert.IsNull(relation.GetVertex("B", "C"));
        }
    }
}

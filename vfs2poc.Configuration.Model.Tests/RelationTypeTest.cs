using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vfs2poc.Configuration.Model.RelationFluentBuilder;

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

            relation.AddEntityType(a, "A");

            Assert.AreEqual(relation.EntityTypes.Count, 1);
            Assert.AreEqual(relation.EntityTypes["A"], a);

            Assert.AreEqual(relation.Vertices.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddNullEntityType()
        {
            var relation = new RelationType();

            relation.AddEntityType(null, "A");
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

            relation.AddEntityType(a, null);
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

            var relation = RelationBuilder.NewRelation("rel1")
                .AddNode(a, "A")
                .JoinsTo(b, "B", 1, 1, 1, 1)
                .Build();

            Assert.AreEqual(relation.EntityTypes.Count, 2);
            Assert.AreEqual(relation.GetEntityType("A"), a);
            Assert.AreEqual(relation.GetEntityType("B"), b);

            Assert.AreEqual(relation.Vertices.Count, 1);

            var vertex = relation.GetVertex("A", "B");
            Assert.AreEqual(vertex.LeftMin, 1);
            Assert.AreEqual(vertex.LeftMax, 1);
            Assert.AreEqual(vertex.RightMin, 1);
            Assert.AreEqual(vertex.RightMax, 1);
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

            var relation = RelationBuilder.NewRelation("rel1")
                .AddNode(a, "A")
                .JoinsTo(b, "B", 0, null, 0, null)
                .Build();

            Assert.AreEqual(relation.EntityTypes.Count, 2);
            Assert.AreEqual(relation.GetEntityType("A"), a);
            Assert.AreEqual(relation.GetEntityType("B"), b);

            Assert.AreEqual(relation.Vertices.Count, 1);

            var vertex = relation.GetVertex("A", "B");
            Assert.AreEqual(vertex.LeftMin, 0);
            Assert.AreEqual(vertex.LeftMax, null);
            Assert.AreEqual(vertex.RightMin, 0);
            Assert.AreEqual(vertex.RightMax, null);
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

            var relation = RelationBuilder.NewRelation("rel1")
                .AddNode(a, "A")
                .JoinsTo(b, "B", 0, null, 0, null)
                .JoinsTo(c, "C", 0, null, 0, null)
                .Build();

            Assert.AreEqual(relation.EntityTypes.Count, 3);
            Assert.AreEqual(relation.GetEntityType("A"), a);
            Assert.AreEqual(relation.GetEntityType("B"), b);
            Assert.AreEqual(relation.GetEntityType("C"), c);

            Assert.AreEqual(relation.Vertices.Count, 2);

            var abVertex = relation.GetVertex("A", "B");
            Assert.AreEqual(abVertex.NodeLeft.Key, "A");
            Assert.AreEqual(abVertex.NodeRight.Key, "B");

            var bcVertex = relation.GetVertex("B", "C");
            Assert.AreEqual(bcVertex.NodeLeft.Key, "B");
            Assert.AreEqual(bcVertex.NodeRight.Key, "C");

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

            var relation = RelationBuilder.NewRelation("rel1")
                .AddNode(a, "A")
                .JoinsTo(b, "B", 0, null, 0, null)
                .GoToNode("A")
                .JoinsTo(c, "C", 0, null, 0, null)
                .Build();

            Assert.AreEqual(relation.EntityTypes.Count, 3);
            Assert.AreEqual(relation.GetEntityType("A"), a);
            Assert.AreEqual(relation.GetEntityType("B"), b);
            Assert.AreEqual(relation.GetEntityType("C"), c);

            Assert.AreEqual(relation.Vertices.Count, 2);

            var abVertex = relation.GetVertex("A", "B");
            Assert.AreEqual(abVertex.NodeLeft.Key, "A");
            Assert.AreEqual(abVertex.NodeRight.Key, "B");

            var acVertex = relation.GetVertex("A", "C");
            Assert.AreEqual(acVertex.NodeLeft.Key, "A");
            Assert.AreEqual(acVertex.NodeRight.Key, "C");

            Assert.IsNull(relation.GetVertex("B", "C"));
        }
    }
}

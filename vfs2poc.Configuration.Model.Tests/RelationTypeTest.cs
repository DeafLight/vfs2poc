using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace vfs2poc.Configuration.Model.Tests
{
    [TestClass]
    public class RelationTypeTest
    {
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

            var relation = RelationBuilder.RelationBuilder.NewRelation("rel1")
                .AddNode(a, "A")
                .JoinsTo(b, "B", 1, 1, 1, 1).Build();

            Assert.AreEqual(relation.EntityTypes.Count, 2);
            Assert.AreEqual(relation.EntityTypes["A"], a);
            Assert.AreEqual(relation.EntityTypes["B"], b);
            Assert.AreEqual(relation.Vertices.Count, 1);

            var vertex = relation.Vertices[0];
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

            var relation = RelationBuilder.RelationBuilder.NewRelation("rel1")
                .AddNode(a, "A")
                .JoinsTo(b, "B", 0, null, 0, null).Build();

            Assert.AreEqual(relation.EntityTypes.Count, 2);
            Assert.AreEqual(relation.EntityTypes["A"], a);
            Assert.AreEqual(relation.EntityTypes["B"], b);
            Assert.AreEqual(relation.Vertices.Count, 1);

            var vertex = relation.Vertices[0];
            Assert.AreEqual(vertex.LeftMin, 0);
            Assert.AreEqual(vertex.LeftMax, null);
            Assert.AreEqual(vertex.RightMin, 0);
            Assert.AreEqual(vertex.RightMax, null);
        }
    }
}

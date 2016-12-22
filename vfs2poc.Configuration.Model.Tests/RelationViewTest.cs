using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vfs2poc.Configuration.Interfaces;
using vfs2poc.Public.DataProviders;

namespace vfs2poc.Configuration.Model.Tests
{
    [TestClass]
    public class RelationViewTest
    {
        [TestMethod]
        public void SimpleRelationWithBasicFields()
        {
            var a = new EntityType
            {
                Code = "A",
                Fields = new List<IField>
                {
                    new ShortTextField
                    {
                        Code="aField1",
                    },
                    new ShortTextField
                    {
                        Code="aField2",
                    },
                },
            };

            var b = new EntityType
            {
                Code = "B",
                Fields = new List<IField>
                {
                    new ShortTextField
                    {
                        Code="bField1",
                    },
                    new ShortTextField
                    {
                        Code="bField2",
                    },
                },
            };

            var relation = new RelationType("rel1");
            relation.AddNode(a, "A");
            relation.AddNode(b, "B");
            relation.AddVertex("A", "B");

            var view = new RelationView
            {
                RelationType = relation,
                Controls = new List<IControl>
                {
                    new FieldControl
                    {
                        Discriminant = "A",
                        Field = a.Fields[0],
                    },
                    new FieldControl
                    {
                        Discriminant = "A",
                        Field = a.Fields[1],
                    },
                    new FieldControl
                    {
                        Discriminant = "B",
                        Field = a.Fields[0],
                    },
                    new FieldControl
                    {
                        Discriminant = "B",
                        Field = a.Fields[1],
                    },
                },
            };

            var dataProvider = new MSSqlQueryDataProvider();

            //var query = dataProvider.GetData(view);

            //Assert.AreEqual(query, $"SELECT {ent1.Fields[1].Code}, {ent1.Fields[0].Code} FROM {ent1.Code}");
        }
    }
}

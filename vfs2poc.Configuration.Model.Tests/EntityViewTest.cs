using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using vfs2poc.Configuration.Interfaces;
using vfs2poc.Public.DataProviders;

namespace vfs2poc.Configuration.Model.Tests
{
    [TestClass]
    public class EntityViewTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AnEmptyViewShouldThrow()
        {
            var view = new EntityView();

            var dataProvider = new MSSqlQueryDataProvider();

            var query = dataProvider.GetData(view);
        }

        [TestMethod]
        public void ViewWithOneEntityAndSimpleControls()
        {
            var ent1 = new EntityType
            {
                Code = "ent1",
                Fields = new List<IField>
                {
                    new ShortTextField
                    {
                        Code="field1",
                    },
                    new ShortTextField
                    {
                        Code="field2",
                    },
                },
            };

            var view = new EntityView
            {
                EntityType = ent1,
                Controls = new List<IControl>
                {
                    new FieldControl
                    {
                        Field = ent1.Fields[1],
                    },
                    new FieldControl
                    {
                        Field = ent1.Fields[0],
                    },
                },
            };

            var dataProvider = new MSSqlQueryDataProvider();

            var query = dataProvider.GetData(view);

            Assert.AreEqual(query, $"SELECT {ent1.Fields[1].Code}, {ent1.Fields[0].Code} FROM {ent1.Code}");
        }
        [TestMethod]
        public void ViewWithOneEntityAndSimpleAndComputedControls()
        {
            var ent1 = new EntityType
            {
                Code = "ent1",
                Fields = new List<IField>
                {
                    new ShortTextField
                    {
                        Code="field1",
                    },
                    new ComputedField
                    {
                        Code="field2",
                        Query="some sql from field2",
                    },
                },
            };

            var view = new EntityView
            {
                EntityType = ent1,
                Controls = new List<IControl>
                {
                    new FieldControl
                    {
                        Field = ent1.Fields[1],
                    },
                    new FieldControl
                    {
                        Field = ent1.Fields[0],
                    },
                },
            };

            var dataProvider = new MSSqlQueryDataProvider();

            var query = dataProvider.GetData(view);

            Assert.AreEqual(query, $"SELECT ({((ComputedField)ent1.Fields[1]).Query}), {ent1.Fields[0].Code} FROM {ent1.Code}");
        }
    }
}

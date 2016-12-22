using System;
using System.Collections.Generic;
using System.Linq;
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

            ent1.Fields.ToList().ForEach(x => x.EntityType = ent1);

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

            ent1.Fields.ToList().ForEach(x => x.EntityType = ent1);

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

        [TestMethod]
        public void ViewOfInheritance()
        {
            var parent = new EntityType
            {
                Code = "parent",
                Fields = new List<IField>
                {
                    new ShortTextField
                    {
                        Code="parentField1",
                    },
                    new ComputedField
                    {
                        Code="parentField2",
                        Query="some sql from parentField2",
                    },
                },
            };

            parent.Fields.ToList().ForEach(x => x.EntityType = parent);

            var child = new EntityType
            {
                Code = "child",
                Fields = new List<IField>
                {
                    new ShortTextField
                    {
                        Code="childField1",
                    },
                    new ComputedField
                    {
                        Code="childField2",
                        Query="some sql from childField2",
                    },
                },
                Parent = parent,
            };

            child.Fields.ToList().ForEach(x => x.EntityType = child);

            parent.Children.Add(child);

            var view = new EntityView
            {
                EntityType = child,
                Controls = new List<IControl>
                {
                    new FieldControl
                    {
                        Field = child.Fields[1],
                    },
                    new FieldControl
                    {
                        Field = child.Fields[0],
                    },
                    new FieldControl
                    {
                        Field = child.Parent.Fields[1],
                    },
                    new FieldControl
                    {
                        Field = child.Parent.Fields[0],
                    },
                },
            };

            var dataProvider = new MSSqlQueryDataProvider();

            var query = dataProvider.GetData(view);

            Assert.AreEqual(query, $"SELECT ({((ComputedField)child.Fields[1]).Query}), {child.Fields[0].Code}, ({((ComputedField)child.Parent.Fields[1]).Query}), {child.Parent.Fields[0].Code} FROM {child.Code} INNER JOIN {parent.Code} ON {parent.Code}.id = {child.Code}.id");
        }
    }
}

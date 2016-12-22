using System;
using System.Collections.Generic;
using System.Linq;
using vfs2poc.Configuration.Interfaces;
using vfs2poc.Configuration.Model;
using vfs2poc.Public.DataProviders.Interfaces;

namespace vfs2poc.Public.DataProviders
{
    public class MSSqlQueryDataProvider : IDataProvider<string>
    {
        public string GetData(IView view)
        {
            return GetData((dynamic)view).Trim();
        }

        protected string GetData(IEntityView view)
        {
            if (!(view?.Controls?.Any()).GetValueOrDefault())
            {
                throw new ArgumentNullException(nameof(view.Controls));
            }

            if (view.EntityType == null)
            {
                throw new ArgumentNullException(nameof(view.EntityType));
            }

            var select = view.Controls.OfType<FieldControl>().Select(x => GetData((dynamic)x.Field));
            var from = view.EntityType.Code;
            var innerJoins = view.Controls.OfType<FieldControl>().Where(x => x.Field.EntityType != view.EntityType).Select(x => x.Field.EntityType).Distinct();
            var where = new List<string>();

            return $"SELECT {string.Join(", ", select)} FROM {from} {string.Join(" ", innerJoins.Select(x => $"INNER JOIN {x.Code} ON {x.Code}.id = {view.EntityType.Code}.id"))}";
        }

        //protected string GetData(IRelationView view)
        //{
        //    if (!(view?.Controls?.Any()).GetValueOrDefault())
        //    {
        //        throw new ArgumentNullException(nameof(view.Controls));
        //    }

        //    if (view.RelationType == null)
        //    {
        //        throw new ArgumentNullException(nameof(view.RelationType));
        //    }

        //    var select = view.Controls.OfType<IFieldControl>().Select(x => GetData((dynamic)x));
        //    var from = view.RelationType.Code;
        //    var innerJoins = view.Controls.OfType<FieldControl>().Where(x => x.Field.EntityType != view.EntityType).Select(x => x.Field.EntityType).Distinct();
        //    var where = new List<string>();

        //    return $"SELECT {string.Join(", ", select)} FROM {from} {string.Join(" ", innerJoins.Select(x => $"INNER JOIN {x.Code} ON {x.Code}.id = {view.EntityType.Code}.id"))}";
        //}

        protected string GetData(IFieldControl control)
        {
            if (control == null)
            {
                throw new ArgumentNullException(nameof(control));
            }

            return GetData((dynamic)control.Field);
        }

        protected string GetData(IField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return field.Code;
        }

        protected string GetData(ComputedField field)
        {
            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            return $"({field.Query})";
        }
    }
}

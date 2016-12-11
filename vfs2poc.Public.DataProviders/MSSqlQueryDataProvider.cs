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
            return GetData((dynamic)view);
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
            var where = new List<string>();

            return $"SELECT {string.Join(", ", select)} FROM {from}";
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

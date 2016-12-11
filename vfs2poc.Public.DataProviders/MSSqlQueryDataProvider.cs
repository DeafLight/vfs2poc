using System;
using System.Collections.Generic;
using System.Linq;
using vfs2poc.Configuration.Model;
using vfs2poc.Public.DataProviders.Interfaces;

namespace vfs2poc.Public.DataProviders
{
    public class MSSqlQueryDataProvider : IDataProvider<string>
    {
        public string GetData(EntityView view)
        {
            if (!(view?.Controls?.Any()).GetValueOrDefault())
            {
                throw new ArgumentNullException(nameof(view.Controls));
            }

            if (view.EntityType == null)
            {
                throw new ArgumentNullException(nameof(view.EntityType));
            }

            var select = view.Controls.OfType<FieldControl>().Select(x => x.Field.Code);
            var from = view.EntityType.Code;
            var where = new List<string>();

            return $"SELECT {string.Join(", ", select)} FROM {from}";
        }
    }
}

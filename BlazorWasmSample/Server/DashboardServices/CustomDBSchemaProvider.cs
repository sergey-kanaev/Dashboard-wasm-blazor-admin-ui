using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BlazorWasmSample.Server.DashboardServices {
    public class CustomDBSchemaProvider : DBSchemaProviderEx {
        public ReadOnlyCollection<string> AllTables = new ReadOnlyCollection<string>(new string[] { "Categories", "Products", "table3" });

        private readonly IEnumerable<string> userDbTables;

        public CustomDBSchemaProvider(IApplicationUserProvider userProvider) : base() {
            userDbTables = userProvider
                .User
                .AvailableDbTables
                .Select(entity => entity.Name);
        }

        public override DBTable[] GetTables(SqlDataConnection connection, params string[] tableList) {
            return base.GetTables(connection, tableList)
                .Where(t => userDbTables.Contains(t.Name))
                .ToArray();
        }
    }
}

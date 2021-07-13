using DevExpress.DataAccess.Sql;
using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWasmSample.Server.DashboardServices {
    public class CustomDBSchemaProvider : DBSchemaProviderEx {
        private readonly IEnumerable<string> availableDbTables;

        public CustomDBSchemaProvider(IApplicationUserProvider userProvider) : base() {
            availableDbTables = userProvider
                .User
                .AvailableDbTables
                .Select(entity => entity.Name);
        }

        public override DBTable[] GetTables(SqlDataConnection connection, params string[] tableList) {
            return base.GetTables(connection, tableList)
                .Where(t => availableDbTables.Contains(t.Name))
                .ToArray();
        }
    }
}

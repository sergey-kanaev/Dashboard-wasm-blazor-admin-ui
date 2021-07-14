using BlazorWasmSample.Server.Models;
using DevExpress.DashboardWeb;

namespace BlazorWasmSample.Server.DashboardServices {
    public class CustomDashboardConfigurator : DashboardConfigurator {
        private readonly ApplicationUser user;

        public CustomDashboardConfigurator(CustomConnectionStringProvider connectionStringProvider, CustomDashboardStorage dashboardStorage, 
            CustomDataSourceStorage dataSourceStorage, CustomDBSchemaProvider dBSchemaProvider, IApplicationUserProvider userProvider) : base() {

            SetConnectionStringsProvider(connectionStringProvider);
            SetDashboardStorage(dashboardStorage);
            SetDataSourceStorage(dataSourceStorage);
           // SetDBSchemaProvider(dBSchemaProvider);

            user = userProvider.User;

            VerifyClientTrustLevel += MultiTenantDashboardConfigurator_VerifyClientTrustLevel;
        }

        private void MultiTenantDashboardConfigurator_VerifyClientTrustLevel(object sender, VerifyClientTrustLevelEventArgs e) {
            if(!user.AllowCreateDashboard && !user.AllowUpdateDashboard && !user.AllowDeleteDashboard)
                e.ClientTrustLevel = ClientTrustLevel.Restricted;
        }
    }
}

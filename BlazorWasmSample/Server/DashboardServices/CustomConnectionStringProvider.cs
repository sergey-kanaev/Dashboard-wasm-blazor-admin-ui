using BlazorWasmSample.Server.Models;
using DevExpress.DataAccess.ConnectionParameters;
using DevExpress.DataAccess.Web;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWasmSample.Server.DashboardServices {
    public class CustomConnectionStringProvider : IDataSourceWizardConnectionStringsProvider {
        private readonly IEnumerable<ConnectionStringEntity> connectionStringEntities;

        public CustomConnectionStringProvider(IApplicationUserProvider userProvider) {
            connectionStringEntities = userProvider.User.ConnectionStrings;
        }

        public Dictionary<string, string> GetConnectionDescriptions() {
            return connectionStringEntities
                .ToDictionary(entity => entity.Name, entity => entity.Description);
        }

        public DataConnectionParametersBase GetDataConnectionParameters(string name) {
            ConnectionStringEntity entity = connectionStringEntities
                .Where(entity => entity.Name == name)
                .FirstOrDefault();

            if(entity != null) {
                return new CustomStringConnectionParameters(entity.ConnectionString);
            } else {
                throw new System.ApplicationException("You are not authorized to use this connection.");
            }
        }
    }
}

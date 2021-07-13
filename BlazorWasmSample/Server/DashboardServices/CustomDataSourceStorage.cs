using DevExpress.DashboardCommon;
using DevExpress.DashboardWeb;
using DevExpress.DataAccess.Json;
using DevExpress.DataAccess.Sql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;

namespace BlazorWasmSample.Server.DashboardServices {
    public class CustomDataSourceStorage : IDataSourceStorage {
        public ReadOnlyCollection<string> AllDataSources = new ReadOnlyCollection<string>(new string[] { sqlDataSourceId1, sqlDataSourceId2, jsonDataSourceId });

        private const string sqlDataSourceId1 = "SQL Data Source (Northwind)";
        private const string sqlDataSourceId2 = "SQL Data Source (CarsXtraScheduling)";
        private const string jsonDataSourceId = "JSON Data Source";

        private readonly IEnumerable<string> userDataSources;
        private Dictionary<string, XDocument> documents = new Dictionary<string, XDocument>();

        public CustomDataSourceStorage(IApplicationUserProvider userProvider) {
            userDataSources = userProvider
                .User
                .AvailableDashboardDataSources
                .Select(entity => entity.Name);

            CreatePredefinedDataSources();
        }

        public IEnumerable<string> GetDataSourcesID() => userDataSources;

        public XDocument GetDataSource(string dataSourceID) {
            if(userDataSources.Contains(dataSourceID)) {
                return documents[dataSourceID];
            } else {
                throw new ApplicationException("You are not authorized to use this datasource.");
            }
        }

        private void CreatePredefinedDataSources() {
            DashboardSqlDataSource sqlDataSource1 = new DashboardSqlDataSource(sqlDataSourceId1, "NorthwindConnectionString");
            SelectQuery query1 = SelectQueryFluentBuilder
                .AddTable("Categories")
                .SelectAllColumnsFromTable()
                .Build("Categories");
            sqlDataSource1.Queries.Add(query1);

            SelectQuery query2 = SelectQueryFluentBuilder
                .AddTable("Products")
                .SelectAllColumnsFromTable()
                .Build("Products");
            sqlDataSource1.Queries.Add(query2);

            DashboardSqlDataSource sqlDataSource2 = new DashboardSqlDataSource(sqlDataSourceId2, "CarsXtraSchedulingConnectionString");
            SelectQuery query = SelectQueryFluentBuilder
                .AddTable("Cars")
                .SelectAllColumnsFromTable()
                .Build("Cars");
            sqlDataSource2.Queries.Add(query);

            DashboardJsonDataSource jsonDataSource = new DashboardJsonDataSource(jsonDataSourceId);
            jsonDataSource.JsonSource = new UriJsonSource(new Uri("https://raw.githubusercontent.com/DevExpress-Examples/DataSources/master/JSON/customers.json"));
            jsonDataSource.RootElement = "Customers";

            documents[sqlDataSourceId1] = new XDocument(sqlDataSource1.SaveToXml());
            documents[sqlDataSourceId2] = new XDocument(sqlDataSource2.SaveToXml());
            documents[jsonDataSourceId] = new XDocument(jsonDataSource.SaveToXml());
        }
    }
}

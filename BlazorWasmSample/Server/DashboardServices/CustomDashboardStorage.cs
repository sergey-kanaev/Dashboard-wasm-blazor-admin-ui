using BlazorWasmSample.Server.Data;
using BlazorWasmSample.Server.Models;
using DevExpress.DashboardWeb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace BlazorWasmSample.Server.DashboardServices {
    public class CustomDashboardStorage : IEditableDashboardStorage {
        private readonly ApplicationUser user;
        private readonly ApplicationDbContext context;

        public CustomDashboardStorage(ApplicationDbContext context, IApplicationUserProvider userProvider) {
            this.user = userProvider.User;
            this.context = context;
        }

        public IEnumerable<DashboardInfo> GetAvailableDashboardsInfo() {
            return user
                .Dashboards
                .Select(dashboardModel => new DashboardInfo() { ID = dashboardModel.Id.ToString(), Name = dashboardModel.Name });
        }

        public XDocument LoadDashboard(string dashboardID) {
            if(user.AllowReadDashboard)
                throw new ApplicationException("You are not authorized to view dashboards.");

            string xml = user
                .Dashboards
                .Where(model => model.Id.ToString() == dashboardID)
                .Select(model => model.XmlContent)
                .FirstOrDefault();

            if(string.IsNullOrEmpty(xml)) {
                throw new ApplicationException("You are not authorized to view this dashboard.");
            } else {
                return XDocument.Parse(xml);
            }
        }

        public string AddDashboard(XDocument dashboard, string dashboardName) {
            if(user.AllowCreateDashboard)
                throw new ApplicationException("You are not authorized to create dashboards.");

            DashboardModel model = new DashboardModel() { Name = dashboardName, XmlContent = dashboard.ToString() };
            user.Dashboards.Add(model);
            context.SaveChanges();
            return model.Id.ToString();
        }

        public void SaveDashboard(string dashboardID, XDocument dashboard) {
            if(user.AllowUpdateDashboard)
                throw new ApplicationException("You are not authorized to modify dashboards.");

            var dashboardModel = user
                .Dashboards
                .Where(model => model.Id.ToString() == dashboardID)
                .FirstOrDefault();

            if(dashboardModel != null) {
                dashboardModel.XmlContent = dashboard.ToString();
                context.SaveChanges();
            }
        }

        public void DeleteDashboard(string dashboardID) {
            if(user.AllowDeleteDashboard)
                throw new ApplicationException("You are not authorized to delete dashboards.");

            var dashboardModel = user
                .Dashboards
                .Where(model => model.Id.ToString() == dashboardID)
                .FirstOrDefault();
            
            if(dashboardModel != null) {
                user.Dashboards.Remove(dashboardModel);
                context.SaveChanges();
            }
        }
    }
}

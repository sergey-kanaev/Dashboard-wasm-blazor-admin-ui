using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorWasmSample.Server.Models {
    public class DashboardModel {
        public string Id { get; set; }
        public string Name { get; set; }
        public string XmlContent { get; set; }

        public ICollection<User_Dashboard> User_Dashboards { get; set; }
    }
}

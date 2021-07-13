using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Models {
    public class User_Dashboard {
        public string DashboardId { get; set; }
        public string UserId { get; set; }
        public DashboardModel Dashboard { get; set; }
        public ApplicationUser User { get; set; }
    }
}

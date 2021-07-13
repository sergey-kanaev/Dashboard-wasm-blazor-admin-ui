using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Models {
    public class ApplicationUser : IdentityUser {
        public bool AllowCreateDashboard { get; set; }
        public bool AllowReadDashboard { get; set; }
        public bool AllowUpdateDashboard { get; set; }
        public bool AllowDeleteDashboard { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<User_Dashboard> User_Dashboards { get; set; }
    }
}

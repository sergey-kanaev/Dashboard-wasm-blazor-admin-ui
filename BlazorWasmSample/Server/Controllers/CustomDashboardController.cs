using BlazorWasmSample.Server.DashboardServices;
using DevExpress.DashboardAspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Controllers {
    [Authorize]
    public class CustomDashboardController : DashboardController {
        public CustomDashboardController(CustomDashboardConfigurator configurator, IDataProtectionProvider dataProtectionProvider) 
            : base(configurator, dataProtectionProvider) {

        }
    }
}

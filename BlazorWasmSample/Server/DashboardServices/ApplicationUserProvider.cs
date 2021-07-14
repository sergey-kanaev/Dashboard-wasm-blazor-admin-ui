using BlazorWasmSample.Server.Data;
using BlazorWasmSample.Server.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.DashboardServices {
    public interface IApplicationUserProvider {
        ApplicationUser User { get; }
    }
    public class ApplicationUserProvider: IApplicationUserProvider {
        private readonly ApplicationUser user;
        public ApplicationUserProvider(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager, ApplicationDbContext context) {
            ClaimsPrincipal principal = contextAccessor.HttpContext.User;
            string userId = userManager.GetUserId(principal) ?? principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            user = userManager.FindByIdAsync(userId).Result;

            context.Entry(user).Collection(user => user.Dashboards).Load();
            context.Entry(user).Collection(user => user.ConnectionStrings).Load();
            context.Entry(user).Collection(user => user.AvailableDbTables).Load();
            context.Entry(user).Collection(user => user.AvailableDashboardDataSources).Load();
        }
        public ApplicationUser User => user;
    }
}

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
        public ApplicationUserProvider(IHttpContextAccessor contextAccessor, UserManager<ApplicationUser> userManager) {
            string userId = contextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            user = userManager.FindByIdAsync(userId).Result;
        }
        public ApplicationUser User => user;
    }
}

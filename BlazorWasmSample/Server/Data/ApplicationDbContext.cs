using BlazorWasmSample.Server.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Data {
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser> {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) {
        }

        public DbSet<DashboardModel> DashboardModels { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            builder
                .Entity<User_Dashboard>()
                .HasKey(ud => new { ud.UserId, ud.DashboardId});
            
            builder
                .Entity<User_Dashboard>()
                .HasOne(ud => ud.User)
                .WithMany(u => u.User_Dashboards)
                .HasForeignKey(ud => ud.UserId);

            builder
                .Entity<User_Dashboard>()
                .HasOne(ud => ud.Dashboard)
                .WithMany(u => u.User_Dashboards)
                .HasForeignKey(ud => ud.DashboardId);
        }
    }
}

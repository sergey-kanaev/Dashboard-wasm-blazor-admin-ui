using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorWasmSample.Server.DashboardServices;
using BlazorWasmSample.Server.Data;
using BlazorWasmSample.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BlazorWasmSample.Server.Areas.Identity.Pages.Account.Manage
{
    public class DashboardPermissionsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly CustomConnectionStringProvider _connectionStringProvider;
        private readonly CustomDataSourceStorage _dataSourceStorage;
        private readonly CustomDBSchemaProvider _dbSchemaProvider;
        private readonly ApplicationDbContext _context;

        public DashboardPermissionsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            CustomDataSourceStorage dataSourceStorage,
            CustomDBSchemaProvider dbSchemaProvider,
            CustomConnectionStringProvider connectionStringProvider,
            ApplicationDbContext context) {
            _userManager = userManager;
            _signInManager = signInManager;
            _connectionStringProvider = connectionStringProvider;
            _dataSourceStorage = dataSourceStorage;
            _dbSchemaProvider = dbSchemaProvider;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            [Display(Name = "Allow Create")]
            public bool AllowCreate { get; set; }
            [Display(Name = "Allow Read")]
            public bool AllowRead { get; set; }
            [Display(Name = "Allow Update")]
            public bool AllowUpdate { get; set; }
            [Display(Name = "Allow Delete")]
            public bool AllowDelete { get; set; }

            public IEnumerable<SelectListItem> Dashboards { get; set; }
            public IEnumerable<string> SelectedDashboards { get; set; } = new List<string>();

            public IEnumerable<SelectListItem> DataSources { get; set; }
            public IEnumerable<string> SelectedDataSources { get; set; } = new List<string>();

            public IEnumerable<SelectListItem> ConnectionStrings { get; set; }
            public IEnumerable<string> SelectedConnectionStrings { get; set; } = new List<string>();

            public IEnumerable<SelectListItem> DbTables { get; set; }
            public IEnumerable<string> SelectedDbTables { get; set; } = new List<string>();
        }

        public async Task<IActionResult> OnGetAsync() {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            // _context.Entry(user).Collection(s => s.AvailableDashboardDataSources).Load();
            Input = new InputModel {
                AllowCreate = user.AllowCreateDashboard,
                AllowRead = user.AllowReadDashboard,
                AllowUpdate = user.AllowUpdateDashboard,
                AllowDelete = user.AllowDeleteDashboard,
                DataSources = _dataSourceStorage.AllDataSources.Select(ds => new SelectListItem(ds, ds)),
                SelectedDataSources = user.AvailableDashboardDataSources.Select(ds => ds.Name),

                Dashboards = _context.DashboardModels.Select(d => new SelectListItem(d.Name, d.Id.ToString())),
                SelectedDashboards = user.Dashboards.Select(d => d.Id.ToString()),

                ConnectionStrings = _context.ConnectionStrings.Select(c => new SelectListItem(c.Name, c.Id.ToString())),
                SelectedConnectionStrings = user.ConnectionStrings.Select(c => c.Id.ToString()),

                DbTables = _dbSchemaProvider.AllTables.Select(t => new SelectListItem(t, t)),
                SelectedDbTables = user.AvailableDbTables.Select(t => t.Name)
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            user.AllowCreateDashboard = Input.AllowCreate;
            user.AllowReadDashboard = Input.AllowRead;
            user.AllowUpdateDashboard = Input.AllowUpdate;
            user.AllowDeleteDashboard = Input.AllowDelete;

            var dataSourcesToRemove = user.AvailableDashboardDataSources.Where(ads => !Input.SelectedDataSources.Any(ds => ds == ads.Name)).ToArray();
            foreach(var entity in dataSourcesToRemove) {
                user.AvailableDashboardDataSources.Remove(entity);
            }
            var dataSourcesToAdd = Input.SelectedDataSources.Where(ds => !user.AvailableDashboardDataSources.Any(ads => ads.Name == ds)).ToArray();
            foreach(string name in dataSourcesToAdd) {
                var entity = new DashboardDataSourceEntity() { Name = name };
                user.AvailableDashboardDataSources.Add(entity);
            }

            var dashboardsToRemove = user.Dashboards.Where(ads => !Input.SelectedDashboards.Any(ds => ds == ads.Id.ToString())).ToArray();
            foreach(var entity in dashboardsToRemove) {
                user.Dashboards.Remove(entity);
            }
            var dashboardIDsToAdd = Input.SelectedDashboards.Where(ds => !user.Dashboards.Any(ads => ads.Id.ToString() == ds));
            var dashboardsToAdd = _context.DashboardModels.Where(ds => dashboardIDsToAdd.Any(id => id == ds.Id.ToString())).ToArray();
            foreach(var dashboard in dashboardsToAdd) {
                user.Dashboards.Add(dashboard);
            }

            var connectionStringsToRemove = user.ConnectionStrings.Where(c => !Input.SelectedConnectionStrings.Any(sc => sc == c.Id.ToString())).ToArray();
            foreach(var entity in connectionStringsToRemove) {
                user.ConnectionStrings.Remove(entity);
            }
            var connectionStringIDsToAdd = Input.SelectedConnectionStrings.Where(sc => !user.ConnectionStrings.Any(c => c.Id.ToString() == sc));
            var connectionStringsToAdd = _context.ConnectionStrings.Where(c => connectionStringIDsToAdd.Any(id => id == c.Id.ToString())).ToArray();
            foreach(var connectionString in connectionStringsToAdd) {
                user.ConnectionStrings.Add(connectionString);
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BlazorWasmSample.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BlazorWasmSample.Server.Areas.Identity.Pages.Account.Manage
{
    public class DashboardPermissionsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public DashboardPermissionsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager) {
            _userManager = userManager;
            _signInManager = signInManager;
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
        }

        public async Task<IActionResult> OnGetAsync() {
            var user = await _userManager.GetUserAsync(User);
            if(user == null) {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Input = new InputModel {
                AllowCreate = user.AllowCreateDashboard,
                AllowRead = user.AllowReadDashboard,
                AllowUpdate = user.AllowUpdateDashboard,
                AllowDelete = user.AllowDeleteDashboard
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
            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}

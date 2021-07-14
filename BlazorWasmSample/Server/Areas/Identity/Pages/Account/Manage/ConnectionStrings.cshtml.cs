using BlazorWasmSample.Server.Data;
using BlazorWasmSample.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Areas.Identity.Pages.Account.Manage {
    public class ConnectionStringsModel : PageModel {
        private readonly ApplicationDbContext _context;

        public ConnectionStringsModel(ApplicationDbContext context) {
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel {
            [Display(Name = "Name")]
            public string Name { get; set; }
            [Display(Name = "Dscription")]
            public string Description { get; set; }
            [Display(Name = "ConnectionName")]
            public string ConnectionString { get; set; }

            public IEnumerable<SelectListItem> ConnectionStrings { get; set; }
        }

        public async Task<IActionResult> OnGetAsync() {
            Input = new InputModel {
                ConnectionStrings = _context.ConnectionStrings.Select(c => new SelectListItem(c.Name, c.Name)),
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() {
            ConnectionStringEntity newCS = new ConnectionStringEntity() {
                Name = Input.Name,
                Description = Input.Description,
                ConnectionString = Input.ConnectionString
            };
            _context.ConnectionStrings.Add(newCS);
            _context.SaveChanges();

            return RedirectToPage();
        }
    }
}

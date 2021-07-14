using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlazorWasmSample.Server.Models {
    public class DashboardDataSourceEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        public DashboardDataSourceEntity() => Users = new List<ApplicationUser>();
    }
}

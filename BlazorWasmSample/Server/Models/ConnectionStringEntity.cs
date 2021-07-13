using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorWasmSample.Server.Models {
    public class ConnectionStringEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ConnectionString { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public ConnectionStringEntity() {
            Users = new List<ApplicationUser>();
        }
    }
}

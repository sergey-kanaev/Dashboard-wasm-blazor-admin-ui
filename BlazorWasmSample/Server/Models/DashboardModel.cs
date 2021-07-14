using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BlazorWasmSample.Server.Models {
    public class DashboardModel {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string XmlContent { get; set; }

        public ICollection<ApplicationUser> Users { get; set; }

        public DashboardModel() => Users = new List<ApplicationUser>();
    }
}

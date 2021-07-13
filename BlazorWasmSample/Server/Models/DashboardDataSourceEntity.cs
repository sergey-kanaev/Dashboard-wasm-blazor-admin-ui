using System.ComponentModel.DataAnnotations;

namespace BlazorWasmSample.Server.Models {
    public class DashboardDataSourceEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

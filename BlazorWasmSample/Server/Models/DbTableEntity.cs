using System.ComponentModel.DataAnnotations;

namespace BlazorWasmSample.Server.Models {
    public class DbTableEntity {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

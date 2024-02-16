using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortalAPI.Model
{
    [Table("Locations")]
    public class Locations
    {
        public int Id { get; set; } = 10030;
        public string? Title { get; set; } 
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? Zip { get; set; }
    }
}

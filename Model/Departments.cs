using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortalAPI.Model
{
    [Table("Departments")]
    public class Departments
    {
        public int Id { get; set; } = 1;
        public string? Title { get; set; } 
    }
}

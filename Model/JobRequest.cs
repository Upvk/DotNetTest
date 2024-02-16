using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JobPortalAPI.Model
{
    [Table("TBL_JOBS")]
    public class JobRequest
    {
        [Key]
        [JsonIgnore]
        public int JobID { get; set; } 
        public string? Title { get; set; } = "eg : Software Developer";
        public string? Description { get; set; } = "eg : 3 years of experience but i have 1.8";
        public int LocationId { get; set; } = 10030;
        public int DepartmentId { get; set; } = 1;
        public DateTime ClosingDate { get; set; } = DateTime.Now;   

        [JsonIgnore]
        public DateTime PostedDate { get; set; }

        [JsonIgnore]
        public string? Code { get; set; }
    }
}

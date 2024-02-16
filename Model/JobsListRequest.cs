namespace JobPortalAPI.Model
{
    public class JobsListRequest
    {
        public string Q { get; set; } = "QA";
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 2;
        public int? LocationId { get; set; } = 10030;
        public int? DepartmentId { get; set; } = 1;
    }
}
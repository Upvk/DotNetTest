using JobPortalAPI.Data;
using JobPortalAPI.Model;


namespace JobPortalAPI.Services
{
    public class JobService
    {
        // Your database context 
        private readonly JobPortalAPIContext dbContext;

        //Constructor
        public JobService(JobPortalAPIContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void CreateJob(JobRequest createJobRequest)
        {
            try
            {
                string jobCode = GenerateNewJobCode();
                // Perform any necessary validation or business logic here before creating the job

                // Create a new Job entity based on the request data
                var newJob = new JobRequest
                {
                    Title = createJobRequest.Title,
                    Description = createJobRequest.Description,
                    LocationId = createJobRequest.LocationId,
                    DepartmentId = createJobRequest.DepartmentId,
                    ClosingDate = createJobRequest.ClosingDate,
                    PostedDate = DateTime.Today,
                    Code = jobCode
                };

                // Add the new job to the database
                dbContext.createJobRequest.Add(newJob);
                dbContext.SaveChanges();
            }
            catch (Exception ex) {
                //handle foreign key contrains
            }
        }

        private string GenerateNewJobCode()
        {
                var lastJob = getLastJob();

                if (lastJob != null)
                {
                    // Parse the last job code and increment it
                    int lastJobCode = int.Parse(lastJob.Code.Substring(4));
                    int newJobCode = lastJobCode + 1;

                    // Create the new job code
                    string formattedNewJobCode = "JOB-" + newJobCode.ToString("D2");

                    return formattedNewJobCode;
                }
                else
                {
                    // No records in the table, start from JOB-01
                    return "JOB-01";
                }
        }

        public JobRequest getLastJob()
        {
            var lastJob = dbContext.createJobRequest
                    .OrderByDescending(j => j.JobID)
                    .FirstOrDefault();

            return lastJob;
        }

        // Method to get a job by ID
        public JobRequest GetJobById(int jobId)
        {
            var job = dbContext.createJobRequest.FirstOrDefault(j => j.JobID == jobId);

            if(job == null)
            {
                return new JobRequest { JobID = jobId , Description = "Job Doesn't Exist"};
            }
            return job;
        }

        // Method to update a job
        public void UpdateJob(JobRequest job)
        {
            try {
                dbContext.createJobRequest.Update(job);
                dbContext.SaveChanges();
            }catch(Exception ex) {
                //handle exceptions
            }    
           
        }


        // Method to get a paginated list of jobs based on filter criteria
        public List<JobRequest> GetJobsList(string searchQuery, int pageNo, int pageSize, int? locationId, int? departmentId)
        {
            IQueryable<JobRequest> query = dbContext.createJobRequest.AsQueryable();

            // Apply filtering based on parameters
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(j => j.Title.Contains(searchQuery) || j.Description.Contains(searchQuery));
            }

            if (locationId.HasValue)
            {
                query = query.Where(j => j.LocationId == locationId.Value);
            }

            if (departmentId.HasValue)
            {
                query = query.Where(j => j.DepartmentId == departmentId.Value);
            }

            // Apply pagination
            query = query.Skip((pageNo - 1) * pageSize).Take(pageSize);

            return query.ToList();
        }

        // Method to get a Department by ID
        public Departments GetDepartmentById(int departmentId)
        {
            var department = dbContext.department.FirstOrDefault(d => d.Id == departmentId);

            if (department == null)
            {
               return new Departments { Id = departmentId , Title = "Department Does not Exist"};
            }

            return department;
        }

        // Method to get a Location by ID
        public Locations GetLocationById(int locationId)
        {
            var location  = dbContext.location.FirstOrDefault(l => l.Id == locationId);

            if (location == null)
            {
                return new Locations { Id = locationId, Title = "LOcation Does not Exist" };
            }

            return location;
        }
    }

}

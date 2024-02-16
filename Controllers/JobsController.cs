using JobPortalAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JobPortalAPI.Services;
using JobPortalAPI.Data;

namespace JobPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobService jobService;

        public JobsController(JobService jobService)
        {
            this.jobService = jobService;
        }

        [HttpPost]
        public IActionResult CreateJob([FromBody] JobRequest createJobRequest)
        {
            // Your logic to create the job using the data from createJobRequest

            jobService.CreateJob(createJobRequest);
            int jobId = jobService.getLastJob().JobID;

            // Return a success response
            return Ok(new
            {
                status = "success",
                message = "Job created successfully",
                data = new
                {
                    jobId,
                    createJobRequest.Title,
                    createJobRequest.Description,
                    createJobRequest.LocationId,
                    createJobRequest.DepartmentId,
                    createJobRequest.ClosingDate
                }
            });
        }


        [HttpPut("{id}")]
        public IActionResult UpdateJob(int id, [FromBody] JobRequest updateJobRequest)
        {
            JobRequest existingJob = jobService.GetJobById(id);

            // Check if the job exists
            if (existingJob == null)
            {
                return NotFound(); 
            }

            // Update the properties
            existingJob.Title = updateJobRequest.Title;
            existingJob.Description = updateJobRequest.Description;
            existingJob.LocationId = updateJobRequest.LocationId;
            existingJob.DepartmentId = updateJobRequest.DepartmentId;
            existingJob.ClosingDate = updateJobRequest.ClosingDate;

            // Call to service to update the job
            jobService.UpdateJob(existingJob);

            return Ok(new
            {
                status = "success",
                message = "Job updated successfully",
                data = existingJob
            });
        }

        [HttpPost("list")]
        public IActionResult GetJobsList([FromBody] JobsListRequest jobsListRequest)
        {
            List<JobRequest> jobsList = jobService.GetJobsList(
                jobsListRequest.Q,
                jobsListRequest.PageNo,
                jobsListRequest.PageSize,
                jobsListRequest.LocationId,
                jobsListRequest.DepartmentId
            );

            var responseModel = new
            {
                jobsList.Count,
                jobsList
            };

            return Ok(new
            {
                status = "success",
                data = responseModel
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetJobDetails(int id)
        {

            var job = jobService.GetJobById(id);
            var department = jobService.GetDepartmentById(job.DepartmentId);
            var location = jobService.GetLocationById(job.LocationId);

            if (job == null)
            {
                return NotFound(); 
            }

            // Map the job entity to the response model
            var responseModel = new
            {
                id = job.JobID,
                code = job.Code,
                title = job.Title,
                description = job.Description,
                location = new
                {
                    id = location.Id,
                    title = location.Title,
                    city = location.City,
                    state = location.State,
                    country = location.Country,
                    zip = location.Zip
                },
                department = new
                {
                    id = department.Id,
                    title = department.Title
                },
                postedDate = job.PostedDate,
                closingDate = job.ClosingDate
            };

            return Ok(new
            {
                status = "success",
                data = responseModel
            });
        }
    }
}

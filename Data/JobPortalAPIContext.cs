using Microsoft.EntityFrameworkCore;
using JobPortalAPI.Model;
using System.Reflection.Emit;

namespace JobPortalAPI.Data
{
    public class JobPortalAPIContext : DbContext
    {
        public JobPortalAPIContext(DbContextOptions<JobPortalAPIContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call HasNoKey for an entity without a primary key
            modelBuilder.Entity<JobsListRequest>().HasNoKey();

            // Other configurations...

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<JobRequest> createJobRequest { get; set; }

        public DbSet<JobsListRequest> jobList { get; set; }

        public DbSet<Locations> location { get; set; }

        public DbSet<Departments> department { get; set; }
    }
}
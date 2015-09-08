using API.Services.Entities;
using System.Data.Entity;

namespace API.Services.Repositories
{
    /// <summary>
    /// Our connection to the db.
    /// </summary>
    class SchoolDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseEnrolment> CourseEnrolments { get; set; }
        public DbSet<CourseWaitinglist> CourseWaitinglists { get; set; }
        public DbSet<CourseTemplate> CourseTemplates { get; set; }
        public DbSet<Student> Students { get; set; }
    }
}

using System.Collections.Generic;
using API.Models;
using API.Services.Repositories;
using System.Linq;
using API.Services.Entities;

namespace API.Services.Providers
{
    /// <summary>
    /// Provides the API with our bussiness logic.
    /// </summary>
    public class CoursesServiceProvider
    {
        private readonly SchoolDbContext _db;

        public CoursesServiceProvider()
        {
            _db = new SchoolDbContext();
        }

        public List<CourseDTO> getAllCourses(string semester = null)
        {
            if (string.IsNullOrEmpty(semester))
            {
                semester = "20153";
            }

            var result = (from c in _db.Courses
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          where c.Semester == semester
                          select new CourseDTO
                          {
                              ID         = c.ID,
                              TemplateID = c.TemplateID,
                              Semester   = c.Semester,
                              Name       = ct.Name
                          }).ToList();

            return result;
        }

        // DUNO
        public CourseDetailDTO addCourse(CourseDetailDTO course)
        {
            return null;
        }

        public CourseDetailDTO getCourseByID(int courseID)
        {
            var result = (from c in _db.Courses
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          where c.ID == courseID
                          select new CourseDetailDTO
                          {
                              ID           = c.ID,
                              TemplateID   = c.TemplateID,
                              Semester     = c.Semester,
                              Name         = ct.Name,
                              StartDate    = c.StartDate,
                              EndDate      = c.EndDate,
                              Students     = getAllStudents(c.ID),
                              StudentCount = getStudentCount(c.ID)
                          }).SingleOrDefault();
            return result;
        }

        public CourseDetailDTO updateCourse(int courseID, CourseDetailDTO course)
        {
            // TODO
            return null;
        }

        public List<StudentDTO> getAllStudents(int courseID)
        {
            var result = (from s in _db.Students
                          join ce in _db.CourseEnrolments on s.ID equals ce.StudentID
                          where ce.CourseID == courseID
                          select new StudentDTO
                          {
                              ID   = s.ID,
                              Name = s.Name,
                              SSN  = s.SSN
                          }).ToList();
            return null;
        }

        public int getStudentCount(int courseID)
        {
            return (from s in _db.CourseEnrolments where s.CourseID == courseID select s).ToList().Count();
        }

        public StudentDTO addStudent(StudentDTO student)
        {
            // TODO
            return null;
        }
    }
}

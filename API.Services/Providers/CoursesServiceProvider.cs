using System.Collections.Generic;
using API.Models.DTOs;
using API.Models.VMs;
using API.Services.Entities;
using API.Services.Repositories;
using System.Linq;
using System;

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

        public void Seed()
        {
            _db.Database.ExecuteSqlCommand("DELETE FROM Courses");
            _db.Database.ExecuteSqlCommand("DELETE FROM Students");
            _db.Database.ExecuteSqlCommand("DELETE FROM CourseTemplates");
            _db.Database.ExecuteSqlCommand("DELETE FROM CourseEnrolments");

            _db.Courses.AddRange(new List<Entities.Course>
            {
                new Entities.Course { TemplateID = "T-514-VEFT", Semester = "20143", StartDate = new DateTime(2014, 8, 15), EndDate = new DateTime(2014, 11, 19) },
                new Entities.Course { TemplateID = "T-514-VEFT", Semester = "20153", StartDate = new DateTime(2015, 8, 17), EndDate = new DateTime(2015, 11, 21) },
                new Entities.Course { TemplateID = "T-111-PROG", Semester = "20143", StartDate = new DateTime(2014, 8, 15), EndDate = new DateTime(2014, 11, 19) }
            });
            _db.SaveChanges();

            _db.CourseTemplates.AddRange(new List<CourseTemplate>
            {
                new CourseTemplate { TemplateID = "T-514-VEFT", Name = "Vefþjónustur" },
                new CourseTemplate { TemplateID = "T-111-PROG", Name = "Forritun" },
            });
            _db.SaveChanges();

            _db.Students.AddRange(new List<Entities.Student>
            {
                new Entities.Student { SSN = "1234567890", Name = "Jón Jónsson" },
                new Entities.Student { SSN = "9876543210", Name = "Guðrún Jónsdóttir" },
                new Entities.Student { SSN = "6543219870", Name = "Gunnar Sigurðsson" },
                new Entities.Student { SSN = "4567891230", Name = "Jóna Halldórsdóttir" }
            });
            _db.SaveChanges();

            _db.CourseEnrolments.AddRange(new List<CourseEnrolment>
            {
                new CourseEnrolment { CourseID = 1, StudentID = 1 },
                new CourseEnrolment { CourseID = 1, StudentID = 2 },
                new CourseEnrolment { CourseID = 2, StudentID = 3 },
                new CourseEnrolment { CourseID = 2, StudentID = 4 },
                new CourseEnrolment { CourseID = 3, StudentID = 1 },
                new CourseEnrolment { CourseID = 3, StudentID = 2 }
            });
            _db.SaveChanges();
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

        public CourseDetailDTO addCourse(CourseDetailViewModel course)
        {
            var newCourse =_db.Courses.Add(new Course {
                TemplateID = course.TemplateID,
                Semester   = course.Semester,
                StartDate  = course.StartDate,
                EndDate    = course.EndDate
            });

            if (newCourse == null)
                throw new CreateFailedException();

            CourseDetailDTO c = new CourseDetailDTO
            {
                ID = newID,
                Name = getCourseName(course.TemplateID),
                TemplateID = course.TemplateID,
                Semester = course.Semester,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            };

            return c;
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

        public CourseDetailDTO updateCourse(int courseID, CourseUpdateDetailViewModel course)
        {
            /*var query = from c in _db.Courses
                        where c.ID == courseID
                        select c;*/
            /*foreach (Course c in query)
            {
                c.TemplateID = course.TemplateID;
                c.Semester = course.Semester;
                c.StartDate = course.StartDate;
                c.EndDate = course.EndDate;
            }
            */


            var res = _db.Courses.SingleOrDefault(c => c.ID == courseID);

            //if(res == null)
            //    throw new NotFoundException();

            res.EndDate = course.EndDate;
            res.StartDate = course.StartDate;
            res.Semester = course.Semester;
            res.TemplateID = course.TemplateID;

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
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

        private int getStudentCount(int courseID)
        {
            return (from s in _db.CourseEnrolments where s.CourseID == courseID select s).ToList().Count();
        }

        private string getCourseName(string templateID)
        {
            return _db.CourseTemplates.SingleOrDefault(ct => ct.TemplateID == templateID).Name;
        }

        public StudentDTO addStudentToCourse(StudentViewModel student)
        {
            //Take courseID in as a parameter to know which course to add to.


            int newID = _db.Students.Add(new Student
            {
                Name = student.Name,
                SSN = student.SSN
            }).ID;

            StudentDTO c = new StudentDTO
            {
                Name = student.Name,
                SSN = student.SSN 
            };

            return c;
           
        }
    }
}

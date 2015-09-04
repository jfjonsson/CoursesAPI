using System.Collections.Generic;
using API.Models.DTOs;
using API.Models.VMs;
using API.Services.Entities;
using API.Services.Repositories;
using System.Linq;
using System;
using API.Services.Exeptions;

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
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE Courses");
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE Students");
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE CourseTemplates");
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE CourseEnrolments");

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
                              ID = c.ID,
                              TemplateID = c.TemplateID,
                              Semester = c.Semester,
                              Name = ct.Name
                          }).ToList();

            return result;
        }

        public CourseDetailDTO addCourse(CourseDetailViewModel course)
        {
            var newCourse = _db.Courses.Add(new Course {
                TemplateID = course.TemplateID,
                Semester = course.Semester,
                StartDate = course.StartDate,
                EndDate = course.EndDate
            });

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new CreateEntryFailedException(e);
            }

            return getCourseByID(newCourse.ID);
        }

        public void removeCourse(int courseID)
        {
            var course = _db.Courses.SingleOrDefault(c => c.ID == courseID);

            if (course == null)
                throw new NotFoundException();
            try
            {
                _db.Courses.Remove(course);
            }
            catch (Exception e)
            {
                throw new EntryRemovalFailedException(e);
            }
        }


        public CourseDetailDTO getCourseByID(int courseID)
        {
            var result = (from c in _db.Courses
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          where c.ID == courseID
                          select new CourseDetailDTO
                          {
                              ID = c.ID,
                              TemplateID = c.TemplateID,
                              Semester = c.Semester,
                              Name = ct.Name,
                              StartDate = c.StartDate,
                              EndDate = c.EndDate
                          }).SingleOrDefault();

            if (result == null)
                throw new NotFoundException();

            result.Students = getAllStudents(result.ID);
            result.StudentCount = result.Students.Count;

            return result;
        }

        public CourseDetailDTO updateCourse(int courseID, CourseUpdateDetailViewModel course)
        {
            var result = _db.Courses.SingleOrDefault(c => c.ID == courseID);

            if (result == null)
                throw new NotFoundException();

            result.EndDate = course.EndDate;
            result.StartDate = course.StartDate;

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new EntryUpdateFailedException(e);
            }

            return getCourseByID(result.ID);
        }

        public List<StudentDTO> getAllStudents(int courseID)
        {
            var course = _db.Courses.SingleOrDefault(c => c.ID == courseID);

            if (course == null)
                throw new NotFoundException();

            var result = (from s in _db.Students
                          join ce in _db.CourseEnrolments on s.ID equals ce.StudentID
                          where ce.CourseID == courseID
                          select new StudentDTO
                          {
                              ID = s.ID,
                              Name = s.Name,
                              SSN = s.SSN
                          }).ToList();

            return result;
        }

        private int getStudentCount(int courseID)
        {
            return (from s in _db.CourseEnrolments where s.CourseID == courseID select s).ToList().Count();
        }

        private string getCourseName(string templateID)
        {
            return _db.CourseTemplates.SingleOrDefault(ct => ct.TemplateID == templateID).Name;
        }

        public List<StudentDTO> addStudentToCourse(int courseID, StudentViewModel studentModel)
        {
            var course = _db.Courses.SingleOrDefault(c => c.ID == courseID);

            var student = _db.Students.SingleOrDefault(s => s.SSN == studentModel.SSN);

            if (course == null || student == null)
                throw new NotFoundException();

            var enroled = _db.CourseEnrolments.SingleOrDefault(e => e.CourseID == course.ID && e.StudentID == student.ID);

            if (enroled != null)
                throw new DuplicateEntryException();

            _db.CourseEnrolments.Add(new CourseEnrolment { CourseID = course.ID, StudentID = student.ID });

            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new CreateEntryFailedException(e);
            }

            return getAllStudents(course.ID);
        }
    }
}

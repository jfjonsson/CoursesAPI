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

        /// <summary>
        /// Seed function to seed that db with some classes and enroled student.
        /// </summary>
        public void Seed()
        {
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE Courses");
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE Students");
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE CourseTemplates");
            _db.Database.ExecuteSqlCommand("TRUNCATE TABLE CourseEnrolments");

            _db.Courses.AddRange(new List<Course>
            {
                new Course { TemplateID = "T-514-VEFT", Semester = "20143", StartDate = new DateTime(2014, 8, 15), EndDate = new DateTime(2014, 11, 19), MaxStudents = 5 },
                new Course { TemplateID = "T-514-VEFT", Semester = "20153", StartDate = new DateTime(2015, 8, 17), EndDate = new DateTime(2015, 11, 21), MaxStudents = 10 },
                new Course { TemplateID = "T-111-PROG", Semester = "20143", StartDate = new DateTime(2014, 8, 15), EndDate = new DateTime(2014, 11, 19), MaxStudents = 5 }
            });
            _db.SaveChanges();

            _db.CourseTemplates.AddRange(new List<CourseTemplate>
            {
                new CourseTemplate { TemplateID = "T-514-VEFT", Name = "Vefþjónustur" },
                new CourseTemplate { TemplateID = "T-111-PROG", Name = "Forritun" },
            });
            _db.SaveChanges();

            _db.Students.AddRange(new List<Student>
            {
                new Student { SSN = "1234567890", Name = "Jón Jónsson" },
                new Student { SSN = "9876543210", Name = "Guðrún Jónsdóttir" },
                new Student { SSN = "6543219870", Name = "Gunnar Sigurðsson" },
                new Student { SSN = "4567891230", Name = "Jóna Halldórsdóttir" },
                new Student { SSN = "1234567890", Name = "Herp McDerpsson 1" },
                new Student { SSN = "1234567891", Name = "Herpina Derpy 1" },
                new Student { SSN = "1234567892", Name = "Herp McDerpsson 2" },
                new Student { SSN = "1234567893", Name = "Herpina Derpy 2" },
                new Student { SSN = "1234567894", Name = "Herp McDerpsson 3" },
                new Student { SSN = "1234567895", Name = "Herpina Derpy 3" },
                new Student { SSN = "1234567896", Name = "Herp McDerpsson 4" },
                new Student { SSN = "1234567897", Name = "Herpina Derpy 4" },
                new Student { SSN = "1234567898", Name = "Herp McDerpsson 5" },
                new Student { SSN = "1234567899", Name = "Herpina Derpy 5" }
            });
            _db.SaveChanges();

            _db.CourseEnrolments.AddRange(new List<CourseEnrolment>
            {
                new CourseEnrolment { CourseID = 1, StudentID = 1, Active = true },
                new CourseEnrolment { CourseID = 1, StudentID = 2, Active = true },
                new CourseEnrolment { CourseID = 2, StudentID = 3, Active = true },
                new CourseEnrolment { CourseID = 2, StudentID = 4, Active = true },
                new CourseEnrolment { CourseID = 3, StudentID = 1, Active = true },
                new CourseEnrolment { CourseID = 3, StudentID = 2, Active = true },
                new CourseEnrolment { CourseID = 1, StudentID = 5, Active = false },
                new CourseEnrolment { CourseID = 2, StudentID = 6, Active = false },
                new CourseEnrolment { CourseID = 3, StudentID = 7, Active = false }
            });
            _db.SaveChanges();

        }

        /// <summary>
        /// Return a list of all the courses in a given semester. Throws Db Exceptions.
        /// </summary>
        /// <param name="semester">String identifier for the semester yyyys y = year s = semester </param>
        /// <returns>A list of CourseDTOs</returns>
        public List<CourseDTO> GetAllCourses(string semester = null)
        {
            if (string.IsNullOrEmpty(semester))
            {
                // Get the current semester.
                semester = "20153";
            }

            // Select the courses, create the CourseDTO from Courses and CourseTemplates tables.
            try
            {
                return (from c in _db.Courses
                              join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                              where c.Semester == semester
                              select new CourseDTO
                              {
                                  ID = c.ID,
                                  TemplateID = c.TemplateID,
                                  Semester = c.Semester,
                                  Name = ct.Name
                              }).ToList();
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        /// <summary>
        /// Add a new course to the db. Throws Db Exceptions.
        /// </summary>
        /// <param name="course">A valid course view model</param>
        /// <returns>The newly created course entry</returns>
        public CourseDetailDTO AddCourse(CourseDetailViewModel course)
        {
            try
            {
                var newCourse = _db.Courses.Add(new Course
                {
                    TemplateID = course.TemplateID,
                    Semester = course.Semester,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate
                });

                _db.SaveChanges();

                return GetCourseById(newCourse.ID);
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        /// <summary>
        /// Attempt to remove a course from the db. Throws NotFound and Db Exceptions.
        /// </summary>
        /// <param name="courseId">The ID of the course to be removed.</param>
        public void RemoveCourse(int courseId)
        {
            var course = _db.Courses.SingleOrDefault(c => c.ID == courseId);

            if (course == null)
                throw new NotFoundException();

            try
            {
                // Remove all students from the course.
                var enroled = _db.CourseEnrolments.Where(ce => ce.CourseID == courseId);
                _db.CourseEnrolments.RemoveRange(enroled);

                _db.Courses.Remove(course);
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        /// <summary>
        /// Retrieve a single course from the db. Throws NotFound and Db Exceptions.
        /// </summary>
        /// <param name="courseId">The ID of the course to be recieved</param>
        /// <returns>The CourseDetailDTO for the course</returns>
        public CourseDetailDTO GetCourseById(int courseId)
        {
            var result = (from c in _db.Courses
                          join ct in _db.CourseTemplates on c.TemplateID equals ct.TemplateID
                          where c.ID == courseId
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

            try
            {
                result.Students = GetAllStudents(result.ID);
                result.StudentCount = result.Students.Count;
                _db.SaveChanges();
            }
            catch (Exception e)
            {

                throw new DbException(e);
            }

            return result;
        }

        /// <summary>
        /// Update the Start and End dates for a single course. Throws NotFound and Db Exceptions.
        /// </summary>
        /// <param name="courseId">The ID of the course to be updated</param>
        /// <param name="course">A valid Update View Model for a course</param>
        /// <returns>The updated CourseDetailDTO</returns>
        public CourseDetailDTO UpdateCourse(int courseId, CourseUpdateDetailViewModel course)
        {
            var result = _db.Courses.SingleOrDefault(c => c.ID == courseId);

            if (result == null)
                throw new NotFoundException();

            try
            {
                result.EndDate = course.EndDate;
                result.StartDate = course.StartDate;
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }

            return GetCourseById(result.ID);
        }

        /// <summary>
        /// Retrieves all active students enroled in a given course. Throws NotFound and Db Exceptions.
        /// </summary>
        /// <param name="courseId">The course in which the students are enroled</param>
        /// <returns>A list of StudentDTOs</returns>
        public List<StudentDTO> GetAllStudents(int courseId)
        {
            var course = _db.Courses.SingleOrDefault(c => c.ID == courseId);

            if (course == null)
                throw new NotFoundException();
            try
            {
                return (from s in _db.Students
                        join ce in _db.CourseEnrolments on s.ID equals ce.StudentID
                        where ce.CourseID == courseId && ce.Active
                        select new StudentDTO
                        {
                            ID = s.ID,
                            Name = s.Name,
                            SSN = s.SSN
                        }).ToList();
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        /// <summary>
        /// Helper function to count the number of tudents in a given course. Throws DbException.
        /// </summary>
        /// <param name="courseId">The ID of the course to count the Students from.</param>
        /// <returns>The number of students in the course.</returns>
        public int GetStudentCount(int courseId)
        {
            try
            {
                return (from s in _db.CourseEnrolments where s.CourseID == courseId select s).ToList().Count;
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        /// <summary>
        /// Helper functoin to retrieve the Name of a course from CourseTemplates. Throws DbException.
        /// </summary>
        /// <param name="templateId">The TemplateID of the course</param>
        /// <returns>The Name pertaining to the given TemplateID</returns>
        public string GetCourseName(string templateId)
        {
            try
            {
                return _db.CourseTemplates.SingleOrDefault(ct => ct.TemplateID == templateId)?.Name;
            }
            catch (Exception e)
            {
                throw new DbException(e);
            }
        }

        /// <summary>
        /// Enrolea new student to a given course. Throws NotFound, DuplicateEntry and DbException.
        /// </summary>
        /// <param name="courseId">The ID of the course in which the student is enroling.</param>
        /// <param name="studentModel">A valid StudentViewModel.</param>
        /// <returns>The updated list of students in the course.</returns>
        public List<StudentDTO> AddStudentToCourse(int courseId, StudentViewModel studentModel)
        {
            var course = _db.Courses.SingleOrDefault(c => c.ID == courseId);

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
                throw new DbException(e);
            }

            return GetAllStudents(course.ID);
        }
    }
}
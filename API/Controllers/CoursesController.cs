using System.Web.Http;
using API.Services.Providers;
using API.Models.DTOs;
using API.Models.VMs;
using System.Net;
using API.Services.Exeptions;
using System;

namespace API.Controllers
{
    /// <summary>
    /// Simple WebAPI for the courses in our school.
    /// </summary>
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private readonly CoursesServiceProvider _service;
        /// <summary>
        /// 
        /// </summary>
        CoursesController()
        {
            _service = new CoursesServiceProvider();
        }

        /// <summary>
        /// Retrieves all the courses for the current semester, or for the
        /// specified semester.
        /// </summary>
        /// <param name="semester">String identifier for the semester yyyys y = year s = semester </param>
        /// <returns>List of simple course details</returns>

        [HttpGet]
        [Route("", Name = "GetAllCourses")]
        public IHttpActionResult GetCourses(string semester = null)
        {
            try
            {
                return Ok(_service.getAllCourses(semester));
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        /// <summary>
        /// Retrieve the course with the specified ID
        /// </summary>
        /// <param name="id">The int ID for the course</param>
        /// <returns>Detail for a single course</returns>
        [HttpGet]
        [Route("{id}", Name = "GetCourseByID")]
        public IHttpActionResult GetCourse(int id)
        {
            try
            {
                return Ok(_service.getCourseByID(id));
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (DbException e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Add a new course.
        /// </summary>
        /// <param name="course">A valid course view model</param>
        /// <returns>Bad request or Created status with resulting course</returns>
        [HttpPost]
        [Route("", Name = "AddCourse")]
        public IHttpActionResult AddCourse([FromBody] CourseDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest("Course model is not valid!");

            try
            {
                var newCourse = _service.addCourse(course);
                return Created(Url.Link("GetCourseByID", new { id = newCourse.ID }), newCourse);
            }
            catch (DbException e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Update an existing course with new data. 
        /// </summary>
        /// <param name="id">The id of the course to be updated</param>
        /// <param name="course">The updated course data</param>
        /// <returns>The updated course and location</returns>
        [HttpPut]
        [Route("{id}", Name = "UpdateCourse")]
        public IHttpActionResult UpdateCourse(int id, [FromBody] CourseUpdateDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest("Course model not valid!");

            try
            {
                return Ok(_service.updateCourse(id, course));
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (DbException e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Attempt to remove a course with the given ID
        /// </summary>
        /// <param name="id">The ID of the course</param>
        /// <returns>No content</returns>
        [HttpDelete]
        [Route("{id}", Name = "RemoveCourse")]
        public IHttpActionResult RemoveCourse(int id)
        {
            try
            {
                _service.removeCourse(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (DbException e)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Get a list of all the students in a course.
        /// </summary>
        /// <param name="id">The ID of the course</param>
        /// <returns>A list of all the students.</returns>
        [HttpGet]
        [Route("{id}/students", Name = "GetStudentsInCourse")]
        public IHttpActionResult GetStudents(int id)
        {
            try
            {
                return Ok(_service.getAllStudents(id));
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Enrole a student in a course.
        /// </summary>
        /// <param name="id">The ID of the course to enrole the student in.</param>
        /// <param name="student">The student view model.</param>
        /// <returns>The updated student list of students for the course.</returns>
        [HttpPost]
        [Route("{id}/students", Name = "AddStudentToCourse")]
        public IHttpActionResult AddStudent(int id, [FromBody] StudentViewModel student)
        {
            if (!ModelState.IsValid)
                return BadRequest("StudentViewModel not valid!");
            try
            {
                return Ok(_service.addStudentToCourse(id, student));
            }
            catch (NotFoundException e)
            {
                return NotFound();
            }
            catch (DuplicateEntryException e)
            {
                return BadRequest("Student already in course!");
            }
            catch (DbException e)
            {
                return InternalServerError();
            }
        }
    }
}
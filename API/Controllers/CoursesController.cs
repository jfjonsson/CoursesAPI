using System.Web.Http;
using API.Services.Providers;
using API.Models.DTOs;
using API.Models.VMs;
using System.Net;

namespace API.Controllers
{
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private readonly CoursesServiceProvider _service;

        CoursesController()
        {
            _service = new CoursesServiceProvider();
        }

        /// <summary>
        /// Retrieves all the courses for the current semester, or for the
        /// specified semester.
        /// </summary>
        /// <returns>List of Course DTO models</returns>
        [HttpGet]
        [Route("", Name = "GetAllCourses")]
        public IHttpActionResult GetCourses(string semester = null)
        {
            return Ok(_service.getAllCourses(semester));
        }

        /// <summary>
        /// Retrieve the course with the specified ID
        /// </summary>
        /// <param name="courseID">The int ID for the course</param>
        /// <returns>Single Course DTO Model</returns>
        [HttpGet]
        [Route("{id}", Name = "GetCourseByID")]
        public IHttpActionResult GetCourse(int id)
        {
            CourseDetailDTO course = _service.getCourseByID(id);
            if (course == null)
                return NotFound();
            return Ok(course);
        }

        /// <summary>
        /// Add a new course and return object and location, otherwise return bad request.
        /// </summary>
        /// <param name="course">A valid course view model</param>
        /// <returns>Bad request or Created status with resulting course</returns>
        [HttpPost]
        [Route("", Name = "AddCourse")]
        public IHttpActionResult AddCourse([FromBody] CourseDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            CourseDetailDTO newCourse = _service.addCourse(course);
            return Created(Url.Link("GetCourseByID", new { id = newCourse.ID } ), newCourse);
        }

        /// <summary>
        /// Update an existing course with new data. 
        /// </summary>
        /// <param name="courseID">The id of the course to be updated</param>
        /// <param name="course">The updated course data</param>
        /// <returns>The updated course and location</returns>
        [HttpPut]
        [Route("{id}", Name = "UpdateCourse")]
        public IHttpActionResult UpdateCourse(int id, [FromBody] CourseUpdateDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            CourseDetailDTO upCourse = _service.updateCourse(id, course);

            if (upCourse == null)
                return NotFound();

            return Ok(upCourse);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}", Name = "RemoveCourse")]
        public IHttpActionResult RemoveCourse(int id)
        {
            bool success = _service.removeCourse(id);

            if (success)
                return StatusCode(HttpStatusCode.NoContent);
            else
                return BadRequest();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/students", Name = "GetStudentsInCourse")]
        public IHttpActionResult GetStudents(int id)
        {

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{id}/students", Name = "AddStudentToCourse")]
        public IHttpActionResult AddStudent(int id, [FromBody] StudentViewModel student)
        {
            return null;
        }
    }
}
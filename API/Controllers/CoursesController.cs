using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Services.Providers;
using API.Models;
using API.Models.VMs;

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
        [Route("")]
        public IEnumerable<CourseDTO> GetCourses(string semester = null)
        {
            return _service.getAllCourses(semester);
        }

        /// <summary>
        /// Retrieve the course with the specified ID
        /// </summary>
        /// <param name="courseID">The int ID for the course</param>
        /// <returns>Single Course DTO Model</returns>
        [HttpGet]
        [Route("{id}")]
        public CourseDetailDTO GetCourseByID(int courseID)
        {
            return _service.getCourseByID(courseID);
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult AddCourse([FromBody] CourseDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            CourseDetailDTO newCourse = _service.addCourse(course);
            return Ok();
        }

        [HttpPut]
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpDelete]
        public void Delete(int id)
        {
        }
    }
}

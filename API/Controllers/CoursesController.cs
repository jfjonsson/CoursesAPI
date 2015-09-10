using System.Web.Http;
using API.Services.Providers;
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
                return Ok(_service.GetAllCourses(semester));
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
                return Ok(_service.GetCourseById(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbException)
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
        public IHttpActionResult AddCourse([FromBody] AddCourseDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest("Course model is not valid!");

            try
            {
                var newCourse = _service.AddCourse(course);
                return Created(Url.Link("GetCourseByID", new { id = newCourse.ID }), newCourse);
            }
            catch (DbException)
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
        public IHttpActionResult UpdateCourse(int id, [FromBody] UpdateCourseDetailViewModel course)
        {
            if (!ModelState.IsValid)
                return BadRequest("Course model not valid!");

            try
            {
                return Ok(_service.UpdateCourse(id, course));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbException)
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
                _service.RemoveCourse(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (DbException)
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
                return Ok(_service.GetAllStudents(id));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Enrole a student in a course.
        /// </summary>
        /// <param name="id">The ID of the course to enrole the student in.</param>
        /// <param name="addStudent">The student view model.</param>
        /// <returns>The updated student list of students for the course.</returns>
        [HttpPost]
        [Route("{id}/students", Name = "EnroleStudent")]
        public IHttpActionResult EnroleStudent(int id, [FromBody] AddStudentViewModel addStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest("StudentViewModel not valid!");
            try
            {
                return Content(HttpStatusCode.Created, _service.AddStudentToCourse(id, addStudent));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (PreconditionFailedException)
            {
                //return new HttpResponseMessage() { Content = new StringContent("bla"), StatusCode = HttpStatusCode.PreconditionFailed };
                return StatusCode(HttpStatusCode.PreconditionFailed);
            }
            catch (DbException)
            {
                return InternalServerError();
            }
        }


        /// <summary>
        /// Disenrole a student from a course. That is set the enrolement status to inactive.
        /// </summary>
        /// <param name="id">The ID of the course from whic the student is being disenroled.</param>
        /// <param name="ssn">The SSN of the student being disenrooled.</param>
        /// <returns>No content</returns>
        [HttpDelete]
        [Route("{id}/students/{ssn}", Name =  "DisenroleStudent")]
        public IHttpActionResult DisenroleStudent(int id, string ssn)
        {
            try
            {
                _service.DisenroleStudent(id, ssn);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NotFoundException)
            {
                    
                return StatusCode(HttpStatusCode.NotFound);
            }
            catch(DbException)
            {
                return InternalServerError();
            }
        }


        /// <summary>
        /// Retrieve the waiting list of students for a course.
        /// </summary>
        /// <param name="id">The ID of the course</param>
        /// <returns>A list of students</returns>
        [HttpGet]
        [Route("{id}/waitinglist", Name = "GetWaitingList")]
        public IHttpActionResult GetWaitingList(int id)
        {
            try
            {
                return Ok(_service.GetWaitinglist(id));
            }
            catch (NotFoundException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            catch (PreconditionFailedException)
            {
                return StatusCode(HttpStatusCode.PreconditionFailed);
            }
            catch (DbException)
            {
                return InternalServerError();
            }
        }


        /// <summary>
        /// Add a student to a courses waiting list.
        /// </summary>
        /// <param name="id">The ID of the course.</param>
        /// <param name="student">The view model for the student being added.</param>
        /// <returns>The updated list of students.</returns>
        [HttpPost]
        [Route("{id}/waitinglist", Name = "AddStudentToWaitinglList")]
        public IHttpActionResult AddStudentToWaitingList(int id, [FromBody] AddStudentViewModel student)
        {
            try
            {
                return Content(HttpStatusCode.OK, _service.AddStudentToWaitingList(id, student));
            }
            catch (NotFoundException)
            {
                return StatusCode(HttpStatusCode.NotFound);
            }
            catch(PreconditionFailedException)
            {
                return StatusCode(HttpStatusCode.PreconditionFailed);
            }
            catch (DbException)
            {
                return InternalServerError();
            }
        }

    }
}
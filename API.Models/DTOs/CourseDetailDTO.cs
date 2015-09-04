using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs
{
    /// <summary>
    /// This class representing a more detailed view of a course at our school.
    /// Contains all the details about a single course, as well as a list of students.
    /// </summary>
    public class CourseDetailDTO
    {
        /// <summary>
        /// Unique identifier for the Course.
        /// Example: 2115
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The name of the course.
        /// Example: "Vefþjónustur"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The teplate identifier for the course.
        /// Example : "T-514-VEFT"
        /// </summary>
        public string TemplateID { get; set; }

        /// <summary>
        /// The semester identifier for the course.
        /// Example: "20153" -> year = 2015 semester = 3
        /// </summary>
        public string Semester { get; set; }

        /// <summary>
        /// The start date of the course.
        /// Example: 2015-08-17
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date of the course.
        /// Example: 2015-10-21
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Number indicating the number of students in the course.
        /// Example: 35
        /// </summary>
        public int StudentCount { get; set; }

        /// <summary>
        /// A list of all the students in the course.
        /// </summary>
        public List<StudentDTO> Students { get; set; }
    }
}

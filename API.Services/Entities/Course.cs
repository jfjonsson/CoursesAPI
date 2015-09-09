using System;

namespace API.Services.Entities
{
    /// <summary>
    /// This class represents a single course at our scool.
    /// </summary>
    class Course
    {
        /// <summary>
        /// Unique identifier for the Course.
        /// Example: 2115
        /// </summary>
        public int ID { get; set; }

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
        /// The maximum number of students that can be enroled in the class at any given time.
        /// Example: 35
        /// </summary>
        public int MaxStudents { get; set; }

    }
}
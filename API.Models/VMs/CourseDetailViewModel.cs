using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.VMs
{
    class CourseDetailViewModel
    {
        /// <summary>
        /// This class representing a more detailed view of a course at our school.
        /// Contains all the details about a single course, as well as a list of students.
        /// </summary>
        public class CourseDetailDTO
        {
            /// <summary>
            /// The name of the course.
            /// Example: "Vefþjónustur"
            /// </summary>
            [Required]
            public string Name { get; set; }

            /// <summary>
            /// The teplate identifier for the course.
            /// Example : "T-514-VEFT"
            /// </summary>
            [Required]
            public string TemplateID { get; set; }

            /// <summary>
            /// The semester identifier for the course.
            /// Example: "20153" -> year = 2015 semester = 3
            /// </summary>
            [Required]
            public string Semester { get; set; }

            /// <summary>
            /// The start date of the course.
            /// Example: 2015-08-17
            /// </summary>
            [Required]
            public DateTime StartDate { get; set; }

            /// <summary>
            /// The end date of the course.
            /// Example: 2015-10-21
            /// </summary>
            [Required]
            public DateTime EndDate { get; set; }
        }
    }
}

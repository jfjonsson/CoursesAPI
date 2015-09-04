using System.ComponentModel.DataAnnotations;

namespace API.Models.VMs
{
    public class CourseViewModel
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
        /// Number indicating the number of students in the course.
        /// Example: 35
        /// </summary>
        [Required]
        public int StudentCount { get; set; }
    }
}

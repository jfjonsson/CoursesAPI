namespace API.Models.DTOs
{
    /// <summary>
    /// This class representing a single course at our school.
    /// Contains various details about the course but not all.
    /// </summary>
    public class CourseDTO
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
        /// Number indicating the number of students in the course.
        /// Example: 35
        /// </summary>
        public int StudentCount { get; set; }
    }
}

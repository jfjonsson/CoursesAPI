namespace API.Models.DTOs
{
    /// <summary>
    /// This class represents a single student at our school
    /// Contains various details about the student.
    /// </summary>
    public class StudentDTO
    {
        /// <summary>
        /// A unique identifier for the student.
        /// Example: 1337
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The students full name.
        /// Example: "Jón Freysteinn Jónsson"
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The students social security number.
        /// Example: "1212922929"
        /// </summary>
        public string SSN { get; set; }
    }
}

namespace API.Services.Entities
{
    /// <summary>
    /// This class represents a single course at our scool.
    /// </summary>
    class Student
    {
        /// <summary>
        /// Unique identifier for the Course.
        /// Example: 2115
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
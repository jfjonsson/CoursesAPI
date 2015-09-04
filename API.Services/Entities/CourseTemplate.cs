namespace API.Services.Entities
{
    /// <summary>
    /// This class represents the name of the course with the given template id.
    /// </summary>
    class CourseTemplate
    {
        /// <summary>
        /// Unique identifier for the CourseTemplate
        /// Example: 1235
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
    }
}
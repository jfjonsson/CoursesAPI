namespace API.Services.Entities
{
    /// <summary>
    /// Relation between students and courses
    /// </summary>
    class CourseEnrolment
    {
        public int ID { get; set; }
        /// <summary>
        /// The ID of the course in which the student is enroled 
        /// </summary>
        public int CourseID { get; set; }

        /// <summary>
        /// The ID of the student that is enroled in the course
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// Indicates whether the student enrolement is active or inactive.
        /// </summary>
        public bool Active { get; set; }
    }
}

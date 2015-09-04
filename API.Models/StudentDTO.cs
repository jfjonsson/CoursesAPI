﻿using System.ComponentModel.DataAnnotations;

namespace API.Models
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
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The students social security number.
        /// Example: "1212922929"
        /// </summary>
        [Required]
        public string SSN { get; set; }
    }
}

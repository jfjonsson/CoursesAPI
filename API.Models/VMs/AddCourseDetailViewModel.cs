﻿using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.VMs
{
    /// <summary>
    /// View Model for adding a new Course to our system.
    /// </summary>
    public class AddCourseDetailViewModel
    {

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

        /// <summary>
        /// The maximum number of students that can be enroled in the class at any given time.
        /// Example: 35
        /// </summary>
        [Required]
        public int MaxStudents { get; set; }
    }
}

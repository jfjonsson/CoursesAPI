﻿using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.VMs
{
    public class CourseUpdateDetailViewModel
    {
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

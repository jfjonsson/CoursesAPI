using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Models.VMs
{
    class Student
    {
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

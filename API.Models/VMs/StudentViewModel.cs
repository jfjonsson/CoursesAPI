using System.ComponentModel.DataAnnotations;

namespace API.Models.VMs
{
    public class StudentViewModel
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

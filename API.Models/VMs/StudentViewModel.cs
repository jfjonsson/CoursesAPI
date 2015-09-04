using System.ComponentModel.DataAnnotations;

namespace API.Models.VMs
{
    public class StudentViewModel
    {
        /// <summary>
        /// The students social security number.
        /// Example: "1212922929"
        /// </summary>
        [Required]
        public string SSN { get; set; }
    }
}

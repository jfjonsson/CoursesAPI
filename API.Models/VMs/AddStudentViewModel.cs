using System.ComponentModel.DataAnnotations;

namespace API.Models.VMs
{
    /// <summary>
    /// A view model for adding an already existing student.
    /// </summary>
    public class AddStudentViewModel
    {
        /// <summary>
        /// The students social security number.
        /// Example: "1212922929"
        /// </summary>
        [Required]
        public string SSN { get; set; }
    }
}

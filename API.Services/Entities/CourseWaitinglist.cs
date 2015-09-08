using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Entities
{
    /// <summary>
    /// 
    /// </summary>
    class CourseWaitinglist
    {
        /// <summary>
        /// The unique identifier of the course waitinglist entry.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// The ID of the student that is on the waiting list for a course.
        /// </summary>
        public int StudentID { get; set; }

        /// <summary>
        /// The ID of the course, for which the student is waiting.
        /// </summary>
        public int CourseID { get; set; }
    }
}

using MP_API.Models;
using System.ComponentModel.DataAnnotations;

namespace MP_API.ReturnModels
{
    public class ReturnCourse
    {
        [Key]
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public string InstructorName { get; set; }
    }
}

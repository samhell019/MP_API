using System.ComponentModel.DataAnnotations;

namespace MP_API.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
        public Instructor Instructor { get; set; }
        public int InstructorId { get; set; }
        public List<Student> Students { get; set; }
    }
}

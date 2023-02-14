using System.ComponentModel.DataAnnotations;

namespace MP_API.Models
{
    public class Drive
    {
        [Key]
        public int DriveId { get; set; }
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Instructor Instructor { get; set; }
        public int InstructorId { get; set;}
        public DateTime Date { get; set; }

    }
}

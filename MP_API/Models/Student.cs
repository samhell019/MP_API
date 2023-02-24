namespace MP_API.Models
{
    public class Student : User
    {
        public Type CourseType { get; set; }
        public Course? StudentCourse { get; set; }
        public int? CourseId { get; set; }
        public List<Drive> Drives { get; set; }
    }
}

namespace MP_API.Models
{
    public class Instructor : User
    {
        public List<Course> Courses { get; set; }
        public List<Drive> Drives { get; set; }

    }
}

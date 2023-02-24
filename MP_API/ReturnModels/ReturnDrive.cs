using MP_API.Models;

namespace MP_API.ReturnModels
{
    public class ReturnDrive
    {
        public int DriveId { get; set; }
        public string StudentName { get; set; }
        public string InstructorName { get; set; }
        public DateTime Date { get; set; }
    }
}

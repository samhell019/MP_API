using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using MP_API.Data;
using MP_API.Models;
using MP_API.ReturnModels;

namespace MP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriveController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DriveController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]

        public ActionResult<List<ReturnDrive>> GetDrives()
        {
            List<ReturnDrive> result = new();
            List<Drive> drives = _context.Drives.ToList();
            List<Student> students = _context.Students.ToList();
            List<Instructor> instructors = _context.Instructors.ToList();
            foreach(Drive drive in drives)
            {
                Student? student = students.FirstOrDefault(s => s.UserId == drive.StudentId);
                Instructor? instructor = instructors.FirstOrDefault(i => i.UserId == drive.InstructorId);
                result.Add(new ReturnDrive()
                {
                    DriveId = drive.DriveId,
                    StudentName = student?.FirstName + " " + student?.LastName,
                    InstructorName = instructor?.FirstName + " " + instructor.LastName,
                    Date = drive.Date
                });
            }
            return Ok(result);
        }

        [HttpGet("{studentId}")]
        public ActionResult<List<ReturnDrive>> GetStudentDrives(int studentId)
        {
            Student? student = _context.Students.FirstOrDefault(s => s.UserId == studentId);
            if(student != null)
            {
                List<ReturnDrive> result = new();
                List<Drive> drives = _context.Drives.Where(d => d.StudentId == student.UserId).ToList(); 
                List<Instructor> instructors = _context.Instructors.ToList();
                foreach (Drive drive in drives)
                {
                    Instructor? instructor = instructors.FirstOrDefault(i => i.UserId == drive.InstructorId);
                    result.Add(new ReturnDrive()
                    {
                        DriveId = drive.DriveId,
                        StudentName = student?.FirstName + " " + student?.LastName,
                        InstructorName = instructor?.FirstName + " " + instructor.LastName,
                        Date = drive.Date
                    });
                }
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet("{driveId}/{studentId}")]
        public ActionResult StudentGetDrive(int driveId, int studentId)
        {
            Student? student = _context.Students.FirstOrDefault(s => s.UserId == studentId);
            if(student != null)
            {
                Drive? drive = _context.Drives.FirstOrDefault(d => d.DriveId == driveId);
                if(drive != null)
                {
                    if(drive.StudentId == null)
                    {
                        drive.StudentId = student.UserId;
                        _context.SaveChanges();
                        return Ok();
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }
    }
}

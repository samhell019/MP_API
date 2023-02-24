using Microsoft.AspNetCore.Mvc;
using MP_API.Data;
using MP_API.Models;
using MP_API.ReturnModels;

namespace MP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CourseController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetExpectantCourses")]
        public ActionResult<List<Course>> GetExpectantCourses()
        {
            List<ReturnCourse> ReturnCourse = new();
            List<Instructor> instructors = _context.Instructors.ToList();
            List<Course> courses = _context.Courses.Where(c => c.Status == Status.Expectant).ToList();
            foreach(var course in courses)
            {
                var instructor = instructors.FirstOrDefault(i => i.UserId == course.InstructorId);
                ReturnCourse.Add(new ReturnCourse
                {
                    CourseId = course.CourseId,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    Status = course.Status,
                    InstructorName = instructor?.FirstName + " " + instructor?.LastName
                });
            }
            return Ok(courses);
        }

        [HttpPost("{CourseId}/{StudentId}")]
        public ActionResult AddStudentToCourse(int StudentId, int CourseId)
        {
            Course? course = _context.Courses.FirstOrDefault(c => c.CourseId == CourseId);
            Student? student = _context.Students.FirstOrDefault(s => s.UserId == StudentId);
            if(student != null && course != null)
            {
                if(student.CourseId != null)
                {
                    return BadRequest();
                }
                student.CourseId = course.CourseId;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{StudentId}")]
        public ActionResult RemoveStudentFromCourse(int StudentId)
        {
            Student? student = _context.Students.FirstOrDefault(s => s.UserId == StudentId);
            if (student != null)
            {
                if (student.CourseId == null)
                {
                    return BadRequest();
                }
                student.CourseId = null;
                _context.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }


    }
}

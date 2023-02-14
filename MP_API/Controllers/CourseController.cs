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
        [HttpGet("GetCourse")]
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
        
        
    }
}

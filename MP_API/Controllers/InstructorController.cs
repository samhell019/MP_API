using Microsoft.AspNetCore.Mvc;
using MP_API.Data;
using MP_API.Models;
using MP_API.ReturnModels;

namespace MP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly AppDbContext _context;
        public InstructorController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Instructors
        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetInstructors()
        {
            List<ReturnInstructor> ReturnInstructors = new();
            var Instructors = _context.Instructors.ToList();
            var Courses = _context.Courses.ToList();
            foreach (var instructor in Instructors)
            {
                ReturnInstructors.Add(new ReturnInstructor
                {
                    FirstName = instructor.FirstName,
                    LastName = instructor.LastName
                });

            }
            return Ok(ReturnInstructors);
        }
    }
}

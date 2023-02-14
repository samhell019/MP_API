using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MP_API.Data;
using MP_API.Models;
using MP_API.ReturnModels;

namespace MP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentController(AppDbContext context)
        {
            _context = context;
        }


        // GET: api/Students
        [HttpGet("GetStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            List<ReturnStudent> ReturnStudents = new();
            var Students = _context.Students.ToList();
            var Courses = _context.Courses.ToList();
            foreach (var student in Students)
            {
                ReturnStudents.Add(new ReturnStudent
                {
                    CourseType = student.CourseType,
                    CourseId = student.CourseId,
                    UserId = student.UserId,
                    FirstName = student.FirstName,
                    LastName = student.LastName
                });

            }
            return Ok(ReturnStudents);
        }

        
    }
}

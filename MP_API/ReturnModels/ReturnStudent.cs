using MP_API.Models;
using System.ComponentModel.DataAnnotations;
using Type = MP_API.Models.Type;

namespace MP_API.ReturnModels
{
    public class ReturnStudent
    {
        public Type CourseType { get; set; }
        public int? CourseId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
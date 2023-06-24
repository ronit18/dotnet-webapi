using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Model;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("all", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(StudentRepository.Students);
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        public ActionResult<Student> GetStudentById(int id)

        {
            if (id <= 0)
            {
                return BadRequest($"Invalid Student Id {id}");
            }

            if (StudentRepository.Students.Where(x => x.Id == id).Count() == 0)
            {
                return NotFound($"Student with Id {id} not found");
            }
            return Ok(StudentRepository.Students.Where(x => x.Id == id).First());
        }

        [HttpDelete("{id:int}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            if (StudentRepository.Students.Where(x => x.Id == id).Count() == 0)
            {
                return NotFound();
            }
            var student = StudentRepository.Students.Where(x => x.Id == id).First();
            StudentRepository.Students.Remove(student);
            return Ok(true);
        }

    }
}
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
        public IEnumerable<Student> GetStudents()
        {
            return StudentRepository.Students;
        }

        [HttpGet("{id:int}", Name = "GetStudentById")]
        public Student GetStudentById(int id)
        {

            return StudentRepository.Students.Where(x => x.Id == id).First();
        }

        [HttpDelete("{id:int}", Name = "DeleteStudentById")]
        public bool DeleteStudentById(int id)
        {

            var student = StudentRepository.Students.Where(x => x.Id == id).First();
            StudentRepository.Students.Remove(student);
            return true;
        }

    }
}
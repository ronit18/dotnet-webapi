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
		[ProducesResponseType(500)]
		[ProducesResponseType(200)]
		public ActionResult<IEnumerable<Student>> GetStudents()
		{
			return Ok(StudentRepository.Students);
		}

		[HttpGet("{id:int}", Name = "GetStudentById")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
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
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Model;
using dotnet_webapi.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StudentController : ControllerBase
	{

		/// <summary>
		/// Get all Students
		/// </summary>
		/// <returns></returns>

		// GET api/student/all
		[HttpGet("all", Name = "GetAllStudents")]
		[ProducesResponseType(500)]
		[ProducesResponseType(200)]
		public ActionResult<IEnumerable<StudentDto>> GetStudents()
		{
			try
			{
				var students = StudentRepository.Students.Select(x => new StudentDto
				{
					Id = x.Id,
					StudentName = x.StudentName,
					Age = x.Age,
					Email = x.Email,
					Address = x.Address
				});

				return Ok(students);
			}
			catch (Exception)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		/// <summary>
		/// Add Student
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>

		// POST api/student/add
		[HttpPost("add", Name = "AddStudent")]
		[ProducesResponseType(201)]
		[ProducesResponseType(409)]
		[ProducesResponseType(500)]
		public ActionResult<StudentDto> CreateStudent([FromBody] StudentDto model)
		{
			try
			{
				if (model == null)
				{
					return BadRequest("Student object is null");
				}


				int newId = StudentRepository.Students.Last().Id + 1;

				var student = new Student
				{
					Id = newId,
					StudentName = model.StudentName,
					Age = model.Age,
					Email = model.Email,
					Address = model.Address,
					AdmissionDate = DateTime.Now
				};

				StudentRepository.Students.Add(student);
				model.Id = student.Id;

				return StatusCode(201, "Created Student Successfully.");

			}
			catch (Exception)
			{
				return StatusCode(500, "Internal Server Error");
			}
		}

		/// <summary>
		/// Get Student by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>

		// GET api/student/{id}
		[HttpGet("{id:int}", Name = "GetStudentById")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public ActionResult<Student> GetStudentById(int id)

		{

			try
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
			catch (Exception)
			{
				return StatusCode(500, "Internal Server Error");
			}

		}


		/// <summary>   
		/// Delete Student by Id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>Deleted Student</returns>

		// DELETE api/student/{id}  
		[HttpDelete("{id:int}", Name = "DeleteStudentById")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public IActionResult DeleteStudentById(int id)
		{
			try
			{
				if (id <= 0)
				{
					return BadRequest($"Invalid Student Id {id}");
				}

				if (StudentRepository.Students.Where(x => x.Id == id).Count() == 0)
				{
					return NotFound($"Student with Id {id} not found");
				}
				var student = StudentRepository.Students.Where(x => x.Id == id).First();
				StudentRepository.Students.Remove(student);
				return Ok("Student Deleted Successfully");
			}
			catch (Exception)
			{
				return StatusCode(500, "Internal Server Error");
			}


		}

		[HttpPut("update", Name = "UpdateStudent")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public ActionResult<StudentDto> UpdateStudent([FromBody] StudentDto model)

		{
			if (model == null || model.Id <= 0)
			{
				return BadRequest("Invalid Student Id");
			}
			var existingStudent = StudentRepository.Students.Where(x => x.Id == model.Id).FirstOrDefault();

			if (existingStudent == null)
			{
				return NotFound($"Student with Id {model.Id} not found");
			}

			existingStudent.StudentName = model.StudentName;
			existingStudent.Age = model.Age;
			existingStudent.Email = model.Email;
			existingStudent.Address = model.Address;


			return Ok("Student Updated Successfully");
		}

		[HttpPatch("{id:int}/updateRequired", Name = "UpdateStudentByPatch")]
		[ProducesResponseType(200)]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(500)]
		public ActionResult<StudentDto> UpdateStudentByPatch(int id, [FromBody] JsonPatchDocument<StudentDto> patchDocument)

		{
			if (patchDocument == null || id <= 0)
			{
				return BadRequest("Invalid Student Id");
			}
			var existingStudent = StudentRepository.Students.Where(x => x.Id == id).FirstOrDefault();

			if (existingStudent == null)
			{
				return NotFound($"Student with Id {id} not found");
			}

			var studentDto = new StudentDto
			{
				Id = existingStudent.Id,
				StudentName = existingStudent.StudentName,
				Email = existingStudent.Email,
				Address = existingStudent.Address,
				Age = existingStudent.Age,
				AddmissionDate = existingStudent.AdmissionDate
			};
			patchDocument.ApplyTo(studentDto, ModelState);
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			existingStudent.StudentName = studentDto.StudentName;
			existingStudent.Age = studentDto.Age;
			existingStudent.Email = studentDto.Email;
			existingStudent.Address = studentDto.Address;

			return Ok("Student Updated Successfully");
		}

	}
}
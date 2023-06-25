using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace dotnet_webapi.Models

{
	public class StudentDto
	{
		[ValidateNever]
		public int Id { get; set; }

		[Required(ErrorMessage = "Student Name is required")]
		[StringLength(30, MinimumLength = 2, ErrorMessage = "Student Name should be minimum 2 characters and a maximum of 30 characters")]
		public string StudentName { get; set; }

		[Required(ErrorMessage = "Student age is required")]
		[Range(18, 60, ErrorMessage = "Student age should be between 18 and 60")]
		public int Age { get; set; }

		[EmailAddress(ErrorMessage = "Invalid Email Address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Address is required")]
		public string Address { get; set; }

		public DateTime AddmissionDate { get; set; } = DateTime.Now;
	}
}
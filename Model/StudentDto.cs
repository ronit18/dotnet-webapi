using System.ComponentModel.DataAnnotations;
namespace dotnet_webapi.Models

{
	public class StudentDto
	{
		public int Id { get; set; }
		[Required]
		public string StudentName { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Required]
		public string Address { get; set; }
	}
}
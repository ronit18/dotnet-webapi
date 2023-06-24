namespace dotnet_webapi.Models
{
	public class Student
	{
		public int Id { get; set; }
		public required string StudentName { get; set; }

		public required string Email { get; set; }

		public required string Address { get; set; }
	}
}
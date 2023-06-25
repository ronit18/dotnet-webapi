using dotnet_webapi.Models;

namespace dotnet_webapi.Model
{
	public static class StudentRepository
	{
		public static List<Student> Students { get; set; } = new List<Student>(){
				new Student{
					Id = 1,
					StudentName = "John",
					Age=20,
					Email = "john@gmail.com",
					Address = "USA",
					AdmissionDate = DateTime.Now
				},
				new Student{
					Id = 2,
					StudentName = "Sam",
					Age=24,
					Email = "sam@gmail.com",
					Address = "USA",
					AdmissionDate = DateTime.Now
				},

			};


	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_webapi.Models;

namespace dotnet_webapi.Model
{
    public static class StudentRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>(){
                new Student{
                    Id = 1,
                    StudentName = "John",
                    Email = "john@gmail.com",
                    Address = "USA"
                },
                new Student{
                    Id = 2,
                    StudentName = "Sam",
                    Email = "sam@gmail.com",
                    Address = "USA"
                },

            };


    }
}
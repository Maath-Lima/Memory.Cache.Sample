using Memory.Cache.Entity;
using System.Collections.Generic;

namespace Memory.Cache.Services
{
    public static class EmployeeService
    {
        public static List<Employee> GetEmployeesDetailsFromDB()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    Id = 1,
                    FirstName = "Test",
                    LastName = "Name",
                    Email = "Test.Name@gmail.com"
                },
                new Employee()
                {
                    Id = 2,
                    FirstName = "Test",
                    LastName = "Name1",
                    Email = "Test.Name1@gmail.com"
                }
            };
        }
    }
}

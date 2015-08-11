namespace SoftUni
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class CreateProject
    {
        public static void AddProject(
            string name,
            string description,
            DateTime startDate,
            List<int> employeesId = null,
            DateTime? endDate = null)
        {
            var db = new SoftUniEntities1();
            var employees = new List<Employee>();

            foreach (var employeeId in employeesId)
            {
                var employee = db.Employees
                    .Where(e => e.EmployeeID == employeeId)
                    .FirstOrDefault();

                if (employee != null)
                {
                    employees.Add(employee);
                }
            }

            var project = new Project
            {
                Name = name,
                Description = description,
                StartDate = startDate,
                EndDate = endDate,
                Employees = employees
            };

            db.Projects.Add(project);
            db.SaveChanges();
            Console.WriteLine("Project added");
        }

    }
}

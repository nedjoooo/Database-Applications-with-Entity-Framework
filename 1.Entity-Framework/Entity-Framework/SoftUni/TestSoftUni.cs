namespace SoftUni
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class TestSoftUni
    {
        static void Main(string[] args)
        {
            var db = new SoftUniEntities1();

            //    Problem 2.	Employee DAO Class

            //    Adding Employee
            //  ModifyngEmployees.AddNewEmployee("Jivko", "Jivkov", "QA Developer", "Production",
            //      new DateTime(2015, 7, 15), 1200, null, "Svetlin Nakov", "Nadezhda 12", "Sofia");

            //  Modifyng Employee

            //  ModifyngEmployees.ModifyEmployee(294, "Marko", "Marchev", null, null, null, 1500, null, null, null, "Nevada");

            //  Remove Employee

            //  ModifyngEmployees.RemoveEmployee(294);

            /*  ----------------------------------------------------------*/

            //  Problem 3.	Employees with Projects after 2002

            /*  var employees = EmployeeSearch.FindEmployeesByProject()
              .OrderBy(e => e.EmployeeID)
              .ToList();

            int countEmployees = employees.Count();
            Console.WriteLine(countEmployees);
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.EmployeeID + ": " + employee.FirstName + " " + employee.LastName);
            } */

            /*  ----------------------------------------------------------*/

            //  Problem 4.	Native SQL Query

            /*  var employeesBySqlNative = EmployeeSearch.NativeSQLQuery()
                .OrderBy(e => e.EmployeeID)
                .ToList();

            int countEmployees = employeesBySqlNative.Count();
            Console.WriteLine(countEmployees);
            foreach (var employee in employeesBySqlNative)
            {
                Console.WriteLine(employee.EmployeeID + ": " + employee.FirstName + " " + employee.LastName);
            }   */

            /*  ----------------------------------------------------------*/

            //  Problem 5.	Employees by Department and Manager

            /*  var employees = EmployeeSearch.EmployeesByDepartmentAndManager("Sales", "Brian Welcker");
            int countEmployees = employees.Count();

            Console.WriteLine(countEmployees);
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.EmployeeID + ": " + employee.FirstName + " " + employee.LastName);
            }   */

            /*  ----------------------------------------------------------*/

            //  Problem 7.	Employees with Corresponding Projects

            //  var employee = db.Employees.Where(e => e.EmployeeID == 290).FirstOrDefault();
            //  Console.WriteLine(employee.ToString());

            /*  ----------------------------------------------------------*/

            //  Problem 8.	Insert a New Project

            //var employeesId = new List<int>
            //{
            //    7, 10
            //};

            //CreateProject.AddProject(
            //    "New Project",
            //    "New Project Description",
            //    new DateTime(2015, 7, 17),
            //    employeesId,
            //    null);

            //var employees = db.Projects
            //    .Where(p => p.ProjectID == 128)
            //    .Select(p => p.Employees)
            //    .ToList();

            //foreach (var employee in employees)
            //{
            //    foreach (var emp in employee)
            //    {
            //        Console.WriteLine(emp.FirstName + emp.LastName);
            //    }
            //}

            /*  ----------------------------------------------------------*/

            //  Problem 9.	Call a Stored Procedure

            var projects = db.usp_SelectEmployeeProjects("JoLynn Dobney").ToList();
            foreach (var project in projects)
            {
                Console.WriteLine(project);
            }
        }
    }
}

namespace SoftUni
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    class EmployeeSearch
    {
        public static IList<Employee> FindEmployeesByProject()
        {
            var db = new SoftUniEntities1();

            var employees = new List<Employee>();

            var projectsEmployees = db.Projects
                .Where(p => (int)(p.StartDate).Year == 2002)
                .Select(p => p.Employees)
                .ToList();

            foreach (var employeesPr in projectsEmployees)
            {
                foreach (var employee in employeesPr)
                {
                    if (!employees.Contains(employee))
                    {
                        employees.Add(employee);
                    }
                }
            }

            return employees.ToList();
        }

        public static IList<Employee> NativeSQLQuery()
        {
            var db = new SoftUniEntities1();

            string nativeQuery = @"SELECT distinct 
                                      e.[EmployeeID],
                                      e.[FirstName],
                                      e.[LastName],
                                      e.[MiddleName],
                                      e.[JobTitle],
                                      e.[DepartmentID],
                                      e.[ManagerID],
                                      e.[HireDate],
                                      e.[Salary],
                                      e.[AddressID]
                                    FROM [SoftUni].[dbo].[Employees] e
                                    join EmployeesProjects ep on ep.EmployeeID = e.EmployeeID
                                    join Projects p on ep.ProjectID = p.ProjectID
                                    where YEAR(p.StartDate) = '2002'";

            var employees = db.Database.SqlQuery<Employee>(nativeQuery);

            return employees.ToList();
        }

        public static IList<Employee> EmployeesByDepartmentAndManager(string department, string manager)
        {
            var db = new SoftUniEntities1();
            // var employees = new List<Employee>();

            int departmentId = db.Departments
                .Where(d => d.Name == department)
                .Select(d => d.DepartmentID)
                .FirstOrDefault();

            int managerId = db.Employees
                .Where(e => (e.FirstName + " " + e.LastName) == manager)
                .Select(e => e.EmployeeID)
                .FirstOrDefault();

            var employees = db.Employees
                .Where(e => e.DepartmentID == departmentId && e.ManagerID == managerId)
                .ToList();

            return employees;
        }
    }
}

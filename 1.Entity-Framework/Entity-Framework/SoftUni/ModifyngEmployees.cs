namespace SoftUni
{
    using System;
    using System.Linq;

    public class ModifyngEmployees
    {
        public static void AddNewEmployee(
            string firstName,
            string lastName,
            string jobTitle,
            string department,
            DateTime hireDate,
            decimal salary,
            string middleName = null,
            string manager = null,
            string address = null,
            string town = null)
        {
            var db = new SoftUniEntities1();
            int newEmployeeDepartmentId = db.Departments
                .Where(d => d.Name == department)
                .Select(d => d.DepartmentID)
                .FirstOrDefault();

            int? employeeManagerId = null;
            int? employeeAddressId = null;
            int? employeeTownId = null;

            if (!string.IsNullOrEmpty(manager))
            {
                employeeManagerId = db.Employees
                    .Where(e => e.FirstName + " " + e.LastName == manager)
                    .Select(e => e.EmployeeID)
                    .FirstOrDefault();
            }

            if (!string.IsNullOrEmpty(address))
            {
                if (!string.IsNullOrEmpty(town))
                {
                    employeeTownId = db.Towns
                        .Where(t => t.Name == town)
                        .Select(t => t.TownID)
                        .FirstOrDefault();
                }

                var employeeAddress = new Address
                {
                    AddressText = address,
                    TownID = employeeTownId
                };

                db.Addresses.Add(employeeAddress);
                db.SaveChanges();

                employeeAddressId = db.Addresses
                    .Max(a => a.AddressID);
            }

            var newEmployee = new Employee
            {
                FirstName = firstName,
                LastName = lastName,
                MiddleName = middleName,
                JobTitle = jobTitle,
                DepartmentID = newEmployeeDepartmentId,
                ManagerID = employeeManagerId,
                HireDate = hireDate,
                Salary = salary,
                AddressID = employeeAddressId
            };

            db.Employees.Add(newEmployee);
            db.SaveChanges();
            Console.WriteLine("Employee added");
        }

        public static void ModifyEmployee(
            int employeeId,
            string firstName = null,
            string lastName = null,
            string jobTitle = null,
            string department = null,
            DateTime? hireDate = null,
            decimal? salary = null,
            string middleName = null,
            string manager = null,
            string address = null,
            string town = null)
        {
            var db = new SoftUniEntities1();
            var employee = db.Employees.Where(e => e.EmployeeID == employeeId).FirstOrDefault();

            if (!string.IsNullOrEmpty(firstName))
            {
                employee.FirstName = firstName;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(lastName))
            {
                employee.LastName = lastName;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(jobTitle))
            {
                employee.JobTitle = jobTitle;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(department))
            {
                int newDepartmentId = db.Departments
                    .Where(d => d.Name == department)
                    .Select(d => d.DepartmentID)
                    .FirstOrDefault();
                employee.DepartmentID = newDepartmentId;
                db.SaveChanges();
            }

            if (hireDate != null)
            {
                employee.HireDate = (DateTime)hireDate;
                db.SaveChanges();
            }

            if (salary != null)
            {
                employee.Salary = (decimal)salary;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(middleName))
            {
                employee.MiddleName = middleName;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(manager))
            {
                int newManagerId = db.Employees
                    .Where(e => e.FirstName + " " + e.LastName == manager)
                    .Select(e => e.EmployeeID)
                    .FirstOrDefault();
                employee.ManagerID = newManagerId;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(address))
            {
                employee.Address.AddressText = address;
                db.SaveChanges();
            }

            if (!string.IsNullOrEmpty(town))
            {
                int newTownId = db.Towns
                    .Where(t => t.Name == town)
                    .Select(t => t.TownID)
                    .FirstOrDefault();
                employee.Address.TownID = newTownId;
                db.SaveChanges();
            }

            Console.WriteLine("Employee modifyng");
        }

        public static void RemoveEmployee(int employeeId)
        {
            var db = new SoftUniEntities1();

            var employee = db.Employees
                .Where(e => e.EmployeeID == employeeId)
                .FirstOrDefault();

            var address = db.Addresses
                .Where(a => a.AddressID == employee.AddressID)
                .FirstOrDefault();

            db.Addresses.Remove(address);
            db.Employees.Remove(employee);
            db.SaveChanges();

            Console.WriteLine("Employee deleted");
        }
    }
}

namespace SoftUni
{
    using System.Text;
    using System.Linq;

    public partial class Employee
    {
        public override string ToString()
        {
            var db = new SoftUniEntities1();

            StringBuilder employee = new StringBuilder();

            string department = db.Departments
                .Where(d => d.DepartmentID == this.DepartmentID)
                .Select(d => d.Name)
                .FirstOrDefault();

            string manager = db.Employees
                .Where(e => e.EmployeeID == this.ManagerID)
                .Select(e => e.FirstName + " " + e.LastName)
                .FirstOrDefault();

            employee.AppendFormat("Id : {0}, Name: {1}", this.EmployeeID, this.FirstName);
            if (!string.IsNullOrEmpty(this.MiddleName))
            {
                employee.AppendFormat(" {0}", this.MiddleName);
            }
            employee.AppendFormat(" {0}, Job Title: {1}, Department: {2}",
                this.LastName,
                this.JobTitle,
                department);

            if (!string.IsNullOrEmpty(manager))
            {
                employee.AppendFormat(", Manager: {0}", manager);
            }

            employee.AppendFormat(", Hire Date: {0}, Salary: {1}", this.HireDate, this.Salary);

            string address = "";
            int? townId = null;

            if (this.AddressID != null)
            {
                townId = db.Addresses
                    .Where(a => a.AddressID == this.AddressID)
                    .Select(a => a.TownID)
                    .FirstOrDefault();

                address = db.Addresses
                    .Where(a => a.AddressID == this.AddressID)
                    .Select(a => a.AddressText)
                    .FirstOrDefault();

                employee.AppendFormat(", Address: {0}", address);
                if (townId != null)
                {
                    string town = db.Towns
                        .Where(t => t.TownID == townId)
                        .Select(t => t.Name)
                        .FirstOrDefault();
                    employee.AppendFormat(", Town: {0}", town);
                }
            }

            return employee.ToString();
        }
    }
}

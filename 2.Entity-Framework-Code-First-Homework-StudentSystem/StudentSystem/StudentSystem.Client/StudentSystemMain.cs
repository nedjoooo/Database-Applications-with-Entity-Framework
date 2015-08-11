namespace StudentSystem.Client
{ 
    using System;
    using System.Linq;
    using StudentSystem.Data;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;

    public class StudentSystemMain
    {
        public static void Main()
        {
            var context = new StudentSystemContext();

            //  1.	Lists all students and their homework submissions. 
            //  Select only their names and for each homework - content and content-type.

            /*
            var students = context.Students
                .Select(s => new { s.Name, s.Homeworks })
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine("--" + student.Name + " - homeworks");
                foreach (var homework in student.Homeworks)
                {
                    Console.WriteLine("Content: " + homework.Content + ", Content Type: " + homework.ContentType.ToString());
                }
                Console.WriteLine();
            }
            */

            //----------------------------------------------------------------------------------------------------------//

            //  2.	List all courses with their corresponding resources. 
            //  Select the course name and description and everything for each resource. Order the courses by start date 

            /*
            var courses = context.Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new { c.Name, c.Description, c.Resources })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine("--" + course.Name);
                Console.WriteLine("-" + course.Description);
                Console.WriteLine("Resources");
                foreach (var resource in course.Resources)
                {
                    Console.WriteLine("Name: " + resource.Name + ", Type: " + resource.Type.ToString() + ", Url: " + resource.Url);
                }
                Console.WriteLine();
            }
            */

            //----------------------------------------------------------------------------------------------------------//

            //  3.	List all courses with more than 5 resources. 
            //  Order them by resources count (descending), then by start date (descending). 
            //  Select only the course name and the resource count.

            /*
            var courses = context.Courses
                .Where(c => c.Resources.Count > 1)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new { c.Name, c.Resources.Count })
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine("Course name: " + course.Name);
                Console.WriteLine("Resources count: " + course.Count);
            }
            */

            //----------------------------------------------------------------------------------------------------------//

            //  4.	List all courses which were active on a given date
            //  (choose the date depending on the data seeded to ensure there are results), 
            //  and for each course count the number of students enrolled. 
            //  Select the course name, start and end date, course duration 
            //  (difference between end and start date) and number of students enrolled. 
            //  Order the results by the number of students enrolled (in descending order), then by duration (descending).

            /*
            var courseDate = new DateTime(2015, 8, 8);
            var courses = context.Courses
                .Where(c => c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now)
                .Select(c => new 
                    { 
                        c.Name,
                        c.StartDate,
                        c.EndDate,
                        Duration = (DbFunctions.DiffDays(c.StartDate, c.EndDate)),
                        c.Students.Count 
                    })
                .OrderByDescending(c => c.Count)
                .OrderByDescending(c => c.Duration)
                .ToList();

            foreach (var course in courses)
            {
                Console.WriteLine("--" + course.Name);
                Console.WriteLine("Start date: " + course.StartDate);
                Console.WriteLine("End date: " + course.EndDate);
                Console.WriteLine("Duration: " + course.Duration);
                Console.WriteLine("Students count: " + course.Count);
                Console.WriteLine();
            }
            */

            //----------------------------------------------------------------------------------------------------------//

            //  5.	For each student, calculate the number of courses he/she has enrolled in, 
            //  the total price of these courses and the average price per course for the student.
            //  Select the student name, number of courses, total price and average price. 
            //  Order the results by total price (descending), then by number of courses (descending) 
            //  and then by the student's name (ascending)

            var students = context.Students
                .Select(s => new
                    {
                        s.Name,
                        s.Courses.Count,
                        CoursesTotalPrice = context.Courses.Sum(c => c.Price),
                        CourseAveragePrice = context.Courses.Average(c => c.Price)
                    })
                .OrderByDescending(s => s.CoursesTotalPrice)
                .ThenByDescending(s => s.Count)
                .ThenBy(s => s.Name)
                .ToList();

            foreach (var student in students)
            {
                Console.WriteLine("Student name: " + student.Name);
                Console.WriteLine("Count of courses: " + student.Count);
                Console.WriteLine("Courses total price: " + student.CoursesTotalPrice);
                Console.WriteLine("Courses average price: " + student.CourseAveragePrice);
                Console.WriteLine();
            }
        }
    }
}

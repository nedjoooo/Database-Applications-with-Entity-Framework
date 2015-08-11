namespace StudentSystem.Data.Migrations
{
    using StudentSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentSystemContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationDataLossAllowed = true;
            this.AutomaticMigrationsEnabled = true;
            this.ContextKey = "StudentSystem.Data.StudentSystemContext";
        }

        protected override void Seed(StudentSystem.Data.StudentSystemContext context)
        {
            //SeedStudents(context);
            //SeedCourses(context);
            //SeedResources(context);
            //SeedHomeworks(context);
        }

        private void SeedStudents(StudentSystem.Data.StudentSystemContext context)
        {
            var studentPesho = new Student
            {
                Name = "Pesho Peshev",
                PhoneNumber = "+359899 123 456",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1990, 5, 5),
            };

            var studentGosho = new Student
            {
                Name = "Gosho Goshev",
                PhoneNumber = "+359899 234 567",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1992, 6, 6),
            };

            var studentMinka = new Student
            {
                Name = "Minka Minkova",
                PhoneNumber = "+359899 345 678",
                RegistrationDate = DateTime.Now,
                Birthday = new DateTime(1994, 7, 7),
            };

            context.Students.Add(studentGosho);
            context.Students.Add(studentPesho);
            context.Students.Add(studentMinka);

            context.SaveChanges();
        }

        private void SeedCourses(StudentSystem.Data.StudentSystemContext context)
        {
            var courseCSharp = new Course
            {
                Name = "C#",
                Description = "C# course",
                StartDate = new DateTime(2015, 7, 8),
                EndDate = new DateTime(2015, 4, 9),
                Price = 120
            };

            var courseJava = new Course
            {
                Name = "Java",
                Description = "Java course",
                StartDate = new DateTime(2015, 7, 20),
                EndDate = new DateTime(2015, 9, 30),
                Price = 140
            };

            var courseJavaScript = new Course
            {
                Name = "JavaScript",
                Description = "JavaScript course",
                StartDate = new DateTime(2015, 7, 1),
                EndDate = new DateTime(2015, 10, 3),
                Price = 99
            };

            context.Courses.Add(courseCSharp);
            context.Courses.Add(courseJava);
            context.Courses.Add(courseJavaScript);

            var studentPesho = context.Students.Where(s => s.Name == "Pesho Peshev").FirstOrDefault();
            studentPesho.Courses.Add(courseCSharp);
            studentPesho.Courses.Add(courseJavaScript);

            var studentGosho = context.Students.Where(s => s.Name == "Gosho Goshev").FirstOrDefault();
            studentGosho.Courses.Add(courseCSharp);
            studentGosho.Courses.Add(courseJava);

            var studentMinka = context.Students.Where(s => s.Name == "Minka Minkova").FirstOrDefault();
            studentMinka.Courses.Add(courseJava);
            studentMinka.Courses.Add(courseJavaScript);

            context.SaveChanges();
        }

        private void SeedResources(StudentSystem.Data.StudentSystemContext context)
        {
            var javaCourseResource = new Resource
            {
                Name = "Java Presentation",
                Type = ResourceType.Presentation,
                Url = "www.resorceJavaScriptPresentation.com"
            };

            var cSharpDocuments = new Resource
            {
                Name = "C# Document",
                Type = ResourceType.Document,
                Url = "www.resorceCSharpLecturesDocument.com"
            };

            var cSharpVideoResource = new Resource
            {
                Name = "C# Video Lection",
                Type = ResourceType.Video,
                Url = "www.resorceCSharpVideoLection.com"
            };

            var javaScriptLab = new Resource
            {
                Name = "JavaScript Lab",
                Type = ResourceType.Other,
                Url = "www.resorceJavaScriptLab.com"
            };

            context.Resources.Add(javaCourseResource);
            context.Resources.Add(cSharpDocuments);
            context.Resources.Add(cSharpVideoResource);
            context.Resources.Add(javaScriptLab);

            var courseCSharp = context.Courses.Where(c => c.Name == "C#").FirstOrDefault();
            courseCSharp.Resources.Add(cSharpDocuments);
            courseCSharp.Resources.Add(cSharpVideoResource);

            var courseJava = context.Courses.Where(c => c.Name == "Java").FirstOrDefault();
            courseJava.Resources.Add(javaCourseResource);

            var courseJavaScript = context.Courses.Where(c => c.Name == "JavaScript").FirstOrDefault();
            courseJavaScript.Resources.Add(javaScriptLab);

            context.SaveChanges();
        }

        private void SeedHomeworks(StudentSystem.Data.StudentSystemContext context)
        {
            var homeworkGoshoOnCSharp = new Homework
            {
                Content = "My CSharp Hello World Homework",
                ContentType = ContentType.ApplicationZip,
                SubmissionDate = new DateTime(2015, 7, 17),
            };

            var homeworkPeshoOnJava = new Homework
            {
                Content = "My Java Arrays Homework",
                ContentType = ContentType.ApplicationPdf,
                SubmissionDate = new DateTime(2015, 7, 18)
            };

            var homeworkMinkaOnJavaScript = new Homework
            {
                Content = "My Advanced JavaScript Homework",
                ContentType = ContentType.ApplicationZip,
                SubmissionDate = new DateTime(2015, 7, 19)
            };

            context.Homeworks.Add(homeworkGoshoOnCSharp);
            context.Homeworks.Add(homeworkPeshoOnJava);
            context.Homeworks.Add(homeworkMinkaOnJavaScript);

            var courseCSharp = context.Courses.Where(c => c.Name == "C#").FirstOrDefault();
            var studentGosho = context.Students.Where(s => s.Name == "Gosho Goshev").FirstOrDefault();
            courseCSharp.Homeworks.Add(homeworkGoshoOnCSharp);
            studentGosho.Homeworks.Add(homeworkGoshoOnCSharp);


            var courseJava = context.Courses.Where(c => c.Name == "Java").FirstOrDefault();
            var studentPesho = context.Students.Where(s => s.Name == "Pesho Peshev").FirstOrDefault();
            courseJava.Homeworks.Add(homeworkPeshoOnJava);
            studentPesho.Homeworks.Add(homeworkPeshoOnJava);

            var courseJavaScript = context.Courses.Where(c => c.Name == "JavaScript").FirstOrDefault();
            var studentMinka = context.Students.Where(s => s.Name == "Minka Minkova").FirstOrDefault();
            courseJavaScript.Homeworks.Add(homeworkMinkaOnJavaScript);
            studentMinka.Homeworks.Add(homeworkMinkaOnJavaScript);

            context.SaveChanges();
        }
    }
}

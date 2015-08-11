namespace ForumSystem.ConsoleClient
{
    using Models;
    using Data;
    using System.Linq;
    using System;

    class Program
    {
        static void Main()
        {
            var context = new ForumContext();

            /*-----------------------------------------------*/
            //var user = new User
            //{
            //    FirstName = "Pesho",
            //    LastName = "Goshev",
            //    Gender = Gender.NotSpedified,
            //    Username = "peshkata",
            //    RegisteredOn = DateTime.Now
            //};

            //context.Users.Add(user);

            /*-----------------------------------------------*/

            //var question = new Question
            //{
            //    Content = "Alcohol",
            //    Title = "daite pls",
            //    AuthorId = 1
            //};

            //context.Questions.Add(question);

            /*-----------------------------------------------*/

            //var question = context.Questions.Find(2);
            //question.Tags.Add(new Tag()
            //    {
            //        Name = "Drugi prikazki"
            //    });

            //question.Tags.Add(new Tag()
            //{
            //    Name = "Vajni saobshteniya"
            //});

            //context.SaveChanges();

            //foreach (var tag in question.Tags)
            //{
            //    Console.WriteLine(tag.Name);
            //}

            /*-----------------------------------------------*/

            //var user = context.Users.Find(1);


            var users = context.Users.ToList();

            foreach (var friend in users[0].Friends)
            {
                Console.WriteLine(friend.FirstName + " " + friend.LastName);
            }

            context.SaveChanges();
        }
    }
}

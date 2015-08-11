namespace ForumSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class User
    {
        private ICollection<Question> questions;
        private ICollection<Answer> answers;
        private ICollection<User> friends;

        public User()
        {
            this.answers = new HashSet<Answer>();
            this.questions = new HashSet<Question>();
            this.friends = new HashSet<User>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public int? Age { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(30)]
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public DateTime RegisteredOn { get; set; }
        public DateTime? Birthday { get; set; }

        public virtual ICollection<Question> Questions
        {
            get { return this.questions; }
            set { this.questions = value; }
        }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }

        public virtual ICollection<User> Friends
        {
            get { return this.friends; }
            set { this.friends = value; }
        }
    }
}

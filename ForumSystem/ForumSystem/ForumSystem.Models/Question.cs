namespace ForumSystem.Models
{
    using System.Collections.Generic;

    public class Question
    {
        private ICollection<Answer> answers;
        private ICollection<Tag> tags;

        public Question()
        {
            this.answers = new HashSet<Answer>();
            this.tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public virtual ICollection<Tag> Tags
        { 
            get { return this.tags; }
            set { this.tags = value; }
        }

        public virtual ICollection<Answer> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }
    }
}

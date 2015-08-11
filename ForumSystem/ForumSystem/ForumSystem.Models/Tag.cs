namespace ForumSystem.Models
{
    using System.Collections.Generic;

    public class Tag
    {
        private ICollection<Question> questions;

        public Tag()
        {
            this.questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Question> Questions 
        {
            get { return this.questions; }
            set { this.questions = value; }
        }
    }
}

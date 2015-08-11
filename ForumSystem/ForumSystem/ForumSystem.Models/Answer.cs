namespace ForumSystem.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}

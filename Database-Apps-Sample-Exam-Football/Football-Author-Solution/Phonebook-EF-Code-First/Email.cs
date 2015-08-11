namespace Phonebook_EF_Code_First
{
    using System.ComponentModel.DataAnnotations;

    public class Email
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string EmailAddress { get; set; }

        public int ContactId { get; set; }

        public virtual Contact Contact { get; set; }
    }
}

namespace JSON.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class User
    {
        private ICollection<User> friends;
        private ICollection<Product> soldProducts;
        private ICollection<Product> boughtProducts;

        public User()
        {
            this.friends = new HashSet<User>();
            this.soldProducts = new HashSet<Product>();
            this.boughtProducts = new HashSet<Product>();
        }

        [Key]
        public int Id { get; set; }

        [MinLength(1)]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string LastName { get; set; }

        public int? Age { get; set; }

        public virtual ICollection<User> Friends
        {
            get { return this.friends; }
            set { this.friends = value; }
        }

        [InverseProperty("Seller")]
        public virtual ICollection<Product> SoldProducts
        {
            get { return this.soldProducts; }
            set { this.soldProducts = value; }
        }

        [InverseProperty("Buyer")]
        public virtual ICollection<Product> BoughtProducts
        {
            get { return this.boughtProducts; }
            set { this.boughtProducts = value; }
        }
    }
}

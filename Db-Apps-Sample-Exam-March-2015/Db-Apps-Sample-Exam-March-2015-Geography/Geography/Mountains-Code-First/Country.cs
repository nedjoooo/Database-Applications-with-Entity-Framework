using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Mountains_Code_First
{
    public class Country
    {
        public Country()
        {
            this.Mountains = new HashSet<Mountain>();
        }

        [Key]
        [MaxLength(2)]
        [MinLength(2)]
        [Column(TypeName = "char")]
        public string Code { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public virtual ICollection<Mountain> Mountains { get; set; }
    }
}

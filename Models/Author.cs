using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class Author: IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Detail { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}

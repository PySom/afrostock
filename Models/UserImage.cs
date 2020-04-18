using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AfrroStock.Models
{
    public class UserImage : IModel
    {
        public UserImage()
        {
            DateAdded = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public int? ImageId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
}

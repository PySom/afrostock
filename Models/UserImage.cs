using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [ForeignKey("UserSubId")]
        public UserSubscription UserSub { get; set; }
        public int UserSubId { get; set; }
        public double Amount { get; set; }
        public DateTime DateAdded { get; set; }
    }
    namespace ViewModels
    {
        public class UserImageVM : IModel
        {
            public int Id { get; set; }
            public int ImageId { get; set; }
            public int? UserSubId { get; set; }
            public double Amount { get; set; }
        }
    }

    namespace DTOs
    {
        public class UserImageDTO
        {
            public int Id { get; set; }
            public int ImageId { get; set; }
            public int UserSubId { get; set; }
            public double Amount { get; set; }
            public DateTime DateAdded { get; set; }
        }
    }

}

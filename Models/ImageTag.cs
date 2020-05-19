using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class ImageTag : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public int? ImageId { get; set; }

        [ForeignKey("TagId")]
        public Tag Tag { get; set; }
        public int TagId { get; set; }
    }
    namespace ViewModels
    {
        public class ImageTagVM
        {
            public int Id { get; set; }
            public int? ImageId { get; set; }
            public int TagId { get; set; }
        }
    }

    namespace DTOs
    {
        public class ImageTagDTO
        {
            public int Id { get; set; }
            public Image Image { get; set; }
            public int? ImageId { get; set; }
            public Tag Tag { get; set; }
            public int TagId { get; set; }
        }
    }

}

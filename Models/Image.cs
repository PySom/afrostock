using AfrroStock.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class Image : IModel
    {
        public Image()
        {
            DateAdded = DateTime.Now;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ContentType ContentType { get; set; }
        public string Content { get; set; }
        public string ContentLow { get; set; }
        public string ContentLower { get; set; }

        [ForeignKey("AuthorId")]
        public ApplicationUser Author { get; set; }
        public int AuthorId { get; set; }
        public int Views { get; set; }
        public double Amount { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }
    }

    namespace ViewModels
    {
        public class ImageVM : IModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public ContentType ContentType { get; set; }
            public string Content { get; set; }
            public string ContentLow { get; set; }
            public string ContentLower { get; set; }
            public int AuthorId { get; set; }
            public double Amount { get; set; }
            public string[] SuggestedTags { get; set; }
        }
    }

    namespace DTOs
    {
        public class ImageDTO : Image
        {}
    }
}

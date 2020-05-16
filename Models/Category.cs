using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class Category : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Image> Images { get; set; }
    }

    namespace ViewModels
    {
        public class CategoryVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }

    namespace DTOs
    {
        public class CategoryDTO : Category
        {}
    }
}

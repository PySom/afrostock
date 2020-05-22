using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class Collection : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("CollectionTypeId")]
        public CollectionType CollectionType { get; set; }
        public int CollectionTypeId { get; set; }
        public ICollection<Collect> Collectibles { get; set; }
    }

    namespace ViewModels
    {
        public class CollectionVM : IModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int CollectionTypeId { get; set; }
        }
    }

    namespace DTOs
    {
        public class CollectionDTO : Collection
        { }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class CollectionType : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Collection> Collections { get; set; }
    }

    namespace ViewModels
    {
        public class CollectionTypeVM : IModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }
    }

    namespace DTOs
    {
        public class CollectionTypeDTO : CollectionType
        {}
    }
}

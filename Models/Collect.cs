using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class Collect : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("CollectionId")]
        public Collection Collection { get; set; }
        public int CollectionId { get; set; }

        [ForeignKey("CollectorId")]
        public ApplicationUser Collector { get; set; }
        public int CollectorId { get; set; }

        [ForeignKey("ImageId")]
        public Image Image { get; set; }
        public int? ImageId { get; set; }
    }

    namespace ViewModels
    {
        public class CollectVM
        {
            public int Id { get; set; }
            public int CollectionId { get; set; }
            public int CollectorId { get; set; }
            public int? ImageId { get; set; }
        }
    }

    namespace DTOs
    {
        public class CollectDTO : Collect
        {}
    }
}

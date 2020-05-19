using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace AfrroStock.Models
{
    public class Tag : IModel, IEqualityComparer<Tag>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ImageTag> ImageTags { get; set; }

        public override string ToString()
        {
            return $"Name: {Name.ToLower()}";
        }

        public bool Equals([AllowNull] Tag x, [AllowNull] Tag y)
        {
            return x.ToString() == y.ToString();
        }

        public int GetHashCode([DisallowNull] Tag obj)
        {
            return obj.ToString().GetHashCode();
        }
    }

    namespace ViewModels
    {
        public class TagVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }

    namespace DTOs
    {
        public class TagDTO
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class Image : IModel
    {
        public Image()
        {
            DateAdded = new DateTime();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateModified { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}

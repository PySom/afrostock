using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class UserSubscription : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public int UserId { get; set; }
        [ForeignKey("SubId")]
        public Subscription Subscription { get; set; }
        public int SubId { get; set; }
        public DateTime StartedOn { get; set; }
    }
}

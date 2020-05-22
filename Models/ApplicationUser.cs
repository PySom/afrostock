using AfrroStock.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AfrroStock.Models
{
    public class ApplicationUser : IModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Details { get; set; }
        public string TwitterHandle { get; set; }
        public string InstagramHandle { get; set; }
        public string FacebookHandle { get; set; }
        public string WebsiteUrl { get; set; }
        public string Email { get; set; }
        public Sex Sex { get; set; }
        public string Pic { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public string Code { get; set; }
        public bool IsVerified { get; set; }
        public DateTime CodeIssued { get; set; }
        public DateTime VerifiedOn { get; set; }
        public DateTime CodeWillExpire { get; set; }
        public ICollection<UserImage> DownloadedImages { get; set; }
        public ICollection<Image> UploadedImages { get; set; }
        public ICollection<Collect> CollectedImages { get; set; }
        public ICollection<UserSubscription> Subscriptions { get; set; }
    }

    namespace DTOs
    {
        public class UserDTO
        {
            public int Id { get; set; }
            public string SurName { get; set; }
            public string FirstName { get; set; }
            public string PhoneNumber { get; set; }
            public string Details { get; set; }
            public string TwitterHandle { get; set; }
            public string InstagramHandle { get; set; }
            public string FacebookHandle { get; set; }
            public string WebsiteUrl { get; set; }
            public string Email { get; set; }
            public Sex Sex { get; set; }
            public string Pic { get; set; }
            public string Role { get; set; }
        }
    }
}

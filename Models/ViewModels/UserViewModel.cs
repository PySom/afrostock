using AfrroStock.Enums;

namespace AfrroStock.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string SurName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Pic { get; set; }
        public Sex Sex { get; set; }
        public string Role { get; set; }
        public bool IsVerified { get; set; }
        public string Token { get; set; }
    }

}

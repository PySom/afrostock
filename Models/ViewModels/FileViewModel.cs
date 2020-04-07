using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace AfrroStock.Models.ViewModels
{
    public class FileViewModel
    {
        [Required]
        public IFormFile File { get; set; }
    }
}

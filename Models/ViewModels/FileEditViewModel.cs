using System.ComponentModel.DataAnnotations;

namespace AfrroStock.Models.ViewModels
{
    public class FileEditViewModel : FileViewModel
    {
        [Required]
        public string OldImage { get; set; }
    }
}

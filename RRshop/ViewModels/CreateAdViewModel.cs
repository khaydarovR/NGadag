using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace NGadag.ViewModels
{
    public class CreateAdViewModel
    {
        [Required]
        public IFormFile? ImageFile { get; set; }
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Descriptions { get; set; }
        public string? Icon { get; set; }
    }
}

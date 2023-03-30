using System.ComponentModel.DataAnnotations;

namespace RRshop.ViewModels
{
    public class CreateProdViewModel
    {

        [Required] public string Title { get; set; }

        [Required] public int CategoryId { get; set; }

        [Required] public float Price { get; set; }

        public string Color { get; set; }
    }
}

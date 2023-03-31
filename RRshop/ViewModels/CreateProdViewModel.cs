using RRshop.Data;
using System.ComponentModel.DataAnnotations;

namespace RRshop.ViewModels
{
    public class CreateProdViewModel
    {

        [Required] public string Title { get; set; }

        [Required] public int CategoryId { get; set; }

        [Required] public float Price { get; set; }

        public List<bool> SizeChose { get; set; } = new List<bool>();

        public string Color { get; set; }
    }
}

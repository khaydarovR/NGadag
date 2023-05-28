using System.ComponentModel.DataAnnotations;

namespace RRshop.ViewModels
{
    public class EditProdViewModel
    {
        public int Id { get; set; }
        [Required] public string Title { get; set; }

        [Required] public int CategoryId { get; set; }

        [Required] public float Price { get; set; }
        public int SaleQuantity { get; set; }

        public string Color { get; set; }

        public bool IsDefaultImage { get; set; }
    }
}

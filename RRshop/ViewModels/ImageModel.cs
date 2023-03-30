using System.ComponentModel.DataAnnotations.Schema;

namespace RRshop.ViewModels
{
    public class ImageModel
    {
        public int ProdId { get; set; }
        [NotMapped] public IFormFile ImageFile { get; set; }

    }
}

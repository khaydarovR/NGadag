namespace RRshop.Models;

public partial class Prod
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public int CategoryId { get; set; }
    public float Price { get; set; }
    public string? Color { get; set; }
    public int SaleQuantity { get; set; }
    public string? ImgPath { get; set; }

    public virtual Category Category { get; set; } = null!;
}

namespace RRshop.Models
{
    public partial class Size
    {
        public int ProdId { get; set; }
        public float Size1 { get; set; }

        public virtual Prod Prod { get; set; } = null!;
    }
}

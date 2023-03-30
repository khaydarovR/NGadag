namespace RRshop.Models
{
    public partial class Category
    {
        public Category()
        {
            Prods = new HashSet<Prod>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        public virtual ICollection<Prod> Prods { get; set; }
    }
}

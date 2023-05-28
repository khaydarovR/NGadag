using System;
using System.Collections.Generic;

namespace NGadag.Models
{
    public partial class Ad
    {
        public Ad()
        {
            AdPhotos = new HashSet<AdPhoto>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Descriptions { get; set; }
        public string? Icon { get; set; }
        public string Photo { get; set; } = null!;

        public virtual ICollection<AdPhoto> AdPhotos { get; set; }
    }
}

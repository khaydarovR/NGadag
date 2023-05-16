using System;
using System.Collections.Generic;

namespace NGadag.Models
{
    public partial class AdPhoto
    {
        public int Id { get; set; }
        public int AdId { get; set; }
        public string Photo { get; set; } = null!;

        public virtual Ad Ad { get; set; } = null!;
    }
}

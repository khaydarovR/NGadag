using System;
using System.Collections.Generic;

namespace RRshop.Models
{
    public partial class UserCart
    {
        public int UserId { get; set; }
        public int ProdId { get; set; }

        public virtual Prod Prod { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}

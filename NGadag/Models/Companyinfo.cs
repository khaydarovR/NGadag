using System;
using System.Collections.Generic;

namespace NGadag.Models
{
    public partial class Companyinfo
    {
        public int Id { get; set; }
        public string InfoType { get; set; } = null!;
        public string InfoValue { get; set; } = null!;
    }
}

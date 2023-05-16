using System;
using System.Collections.Generic;

namespace NGadag.Models
{
    public partial class Application
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string Message { get; set; } = null!;
    }
}

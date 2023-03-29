using System;
using System.Collections.Generic;

namespace RRshop.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string Password { get; set; } = null!;
        public DateTime? CreateTime { get; set; }
        public string? Role { get; set; }
    }
}

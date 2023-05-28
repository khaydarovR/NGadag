using System;
using System.Collections.Generic;

namespace NGadag.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? City { get; set; }
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}

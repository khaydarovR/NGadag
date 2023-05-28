using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace NGadag.Models
{
    public partial class Applications
    {
        public int Id { get; set; }
        [Display(Name = "Загаловок")]
        public string Name { get; set; } = null!;
        [Display(Name = "Ваш email")]
        public string Email { get; set; } = null!;
        [Display(Name = "Номер телефона")]
        [Required]
        public string? Phone { get; set; }
        [Display(Name = "Сообщение")]
        [Required]
        public string Message { get; set; } = null!;
    }
}

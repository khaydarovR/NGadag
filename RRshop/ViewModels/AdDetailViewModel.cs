﻿using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NGadag.ViewModels
{
    public class AdDetailViewModel
    {
        [Display(Name = "ID")]
        public int ID { get; set; }

        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Шапка")]
        public IFormFile HeadImage { get; set; } = null!;
        public int Id { get; set; }
        [Display(Name = "Название")]
        public string Title { get; set; } = null!;
        [Display(Name = "Подробное описание")]
        public string? Descriptions { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [Display(Name = "Иконка")]
        public IFormFile Icon { get; set; } = null!;

        [Display(Name = "Примеры работ")]
        public List<IFormFile>? Samples { get; set; }
    }
}

using GameWebProject.Models.SaleModel;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebProject.ViewModels
{
    public class ItemCreateViewModel
    {
        [Required, MinLength(1)]
        [Display(Name = "Name")]
        public string ProductName { get; set; }
        [Required]
        public Cat? Category { get; set; }
        [Required]
        public Plat? Platform { get; set; }
        [Required]
        public Cond? Condition { get; set; }
        [Required, MinLength(1)]
        public string LookingFor { get; set; }
        public IFormFile Photo{ get; set; }
    }
}

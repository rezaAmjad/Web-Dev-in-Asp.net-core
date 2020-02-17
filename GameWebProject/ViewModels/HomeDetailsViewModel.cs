using GameWebProject.Models.SaleModel;
using GameWebProject.Models.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebProject.ViewModels
{
    public class HomeDetailsViewModel
    {
        public Item Item { get; set; }
        public ApplicationUser ApplicationUser { get; }
        public string PageTitle { get; set; }
    }
}


using GameWebProject.Models.SaleModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
/* this class adds an extention to the ModelBuilder class to seed data and populate the database
 * The reason for using this class is ti keep the AppContext class clean
 */
namespace GameWebProject.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                new Item
                {
                    Id = 1,
                    ProductName = "God of War III",
                    Category = Cat.Game,
                    Platform = Plat.PS4,
                    Condition = Cond.Used,
                    LookingFor = "Fifa19"
                }
                );

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebProject.Models.SaleModel
{
    public interface IItemRepository
    {
        Item GetItem(int Id);
        IEnumerable<Item> GetItems();
        Item Add(Item item);
        Item Update(Item itemChanges);
        Item Delete(int Id);
        IEnumerable<Item> SearchItems(string search);





    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebProject.Models.SaleModel
{
    public class MockItemRepository : IItemRepository
    {
        private List<Item> _itemList;
        public MockItemRepository()
        {
            _itemList = new List<Item> {
                new Item() {Id = 1, ProductName = "Fifa20", Category = Cat.Game, Platform = Plat.PS4, Condition = Cond.Used, LookingFor = "Advance Warefare"},
                new Item() {Id = 2, ProductName = "DarksidersII", Category = Cat.Game, Platform = Plat.PS4, Condition = Cond.Used, LookingFor = "Hitman2"},
                new Item() {Id = 3, ProductName = "Fifa18", Category = Cat.Game, Platform = Plat.PS4, Condition = Cond.Used, LookingFor = "Hitman"}
            };
        }

        public Item Add(Item item)
        {
           item.Id = _itemList.Max(e => e.Id) + 1;
            _itemList.Add(item);
            return item;
        }

        public Item Delete(int Id)
        {
            Item item = _itemList.FirstOrDefault(e => e.Id == Id);
            if(item != null)
            {
                _itemList.Remove(item);
            }
            return item;
        }

        public Item GetItem(int Id)
        {

            return _itemList.FirstOrDefault(e => e.Id == Id);
        }

        public IEnumerable<Item> GetItems()
        {
            return _itemList;
        }

        public IEnumerable<Item> SearchItems(string search)
        {
            throw new NotImplementedException();
        }

        public Item Update(Item itemChanges)
        {
            Item item = _itemList.FirstOrDefault(e => e.Id == itemChanges.Id);
            if (item != null)
            {
                item.ProductName = itemChanges.ProductName;
                item.Category = itemChanges.Category;
                item.Platform = itemChanges.Platform;
                item.Condition = itemChanges.Condition;
                item.LookingFor = itemChanges.LookingFor;

            }
            return item;
        }
    }
}

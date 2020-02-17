using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWebProject.Models.SaleModel
{
    public class SqlItemRepository : IItemRepository
    {
        private readonly AppDbContext context;

        public SqlItemRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Item Add(Item item)
        {
            context.Items.Add(item);
            context.SaveChanges();
            return item;
        }

        public Item Delete(int Id)
        {
            Item item = context.Items.Find(Id);
            if(item != null)
            {
                context.Items.Remove(item);
                context.SaveChanges();
            }
            return item;
        }

        public Item GetItem(int Id)
        {
           return context.Items.Find(Id);
        }

        public IEnumerable<Item> GetItems()
        {
            return context.Items;
        }

        public IEnumerable<Item> SearchItems(string search)
        {
            return context.Items.Where<Item>(e => e.ProductName.Contains(search)).ToList();
        }

        public Item Update(Item itemChanges)
        {
            var item = context.Items.Attach(itemChanges);
            item.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return itemChanges;

        }
    }
}

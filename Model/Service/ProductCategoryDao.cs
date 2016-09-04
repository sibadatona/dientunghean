using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x=>x.Status == false).ToList();
        }
    }
}

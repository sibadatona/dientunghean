using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class MenuDao
    {
        OnlineShopDbContext db = null;
        public MenuDao()
        {
            db = new OnlineShopDbContext();
        }

        public List<Menu> ListByGroupID(string typeID)
        {
            return db.Menus.Where(x => x.TypeID == typeID).ToList();
        }
    }
}

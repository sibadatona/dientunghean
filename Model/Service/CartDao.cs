using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Dao
{
    public class CartDao
    {
        OnlineShopDbContext db = null;

        public CartDao()
        {
            db = new OnlineShopDbContext();
        }

        public int InsertOrder(Order entity)
        {
            try
            {
                db.Orders.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public bool InsertOrderDetail(OrderDetail entity)
        {
            try
            {
                db.OrderDetails.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }
    }
}

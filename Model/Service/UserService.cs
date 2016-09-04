using System;
using System.Linq;
using Model.EF;

namespace Model.Service
{
    public class UserService
    {
        OnlineShopDbContext db = null;

        public UserService()
        {
            db = new OnlineShopDbContext();
        }
        public int Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }

        public bool Update(User user)
        {
            try
            {
                var entity = db.Users.Find(user.ID);
                entity.Name = user.Name;
                entity.Password = user.Password;
                entity.Address = user.Address;
                entity.Email = user.Email;
                entity.ModifileDate = DateTime.Now;
                entity.Status = user.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public PagedResult<User> LoadData(string searchString,string staus, int page, int pageSize)
        {
            IQueryable<User> model = db.Users.Where(x => x.Role != 99);

            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            if (!string.IsNullOrEmpty(staus))
            {
               var statusBool =  bool.Parse(staus);
                model = model.Where(x => x.Status == statusBool);
            }
            return model.OrderByDescending(x => x.ID).ExecutePagingResult(page, pageSize);
        }

        public User ViewDetail(int id)
        {
            return db.Users.Find(id);
        }

        public User GetID(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
        public bool Login(string UserName, string Password)
        {
            var result = db.Users.Count(x => x.UserName == UserName && x.Password == Password && x.Role == 99);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return !user.Status;
        }
    }
}

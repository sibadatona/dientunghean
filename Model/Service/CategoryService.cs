using Model.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Service
{
    public class CategoryService
    {

        OnlineShopDbContext db = null;

        public CategoryService()
        {
            db = new OnlineShopDbContext();
        }

        public List<Category> ListAll()
        {
            return db.Categories.Where(x => x.Status == false).ToList();
        }

        public int Insert(Category category)
        {
            category.MetaTitle = Utility.ConvertToUnSign(category.Name);
            category.CreateDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public Category ViewDetail(int id)
        {
            return db.Categories.Find(id);
        }

        public void Delete(int id)
        {
            var entity = db.Categories.Find(id);
            entity.Status = true;
            db.SaveChanges();
        }

        public void Update(Category cate)
        {
            var entity = db.Categories.Find(cate.ID);
            entity.Name = cate.Name;
            entity.MetaTitle = Utility.ConvertToUnSign(cate.Name);
            entity.DisplayOrder = cate.DisplayOrder;
            entity.ShowOnHome = cate.ShowOnHome;
            db.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Model.EF;
using Model.ViewModel;

namespace Model.Service
{
    public class ProductService
    {
        OnlineShopDbContext db = null;

        public ProductService()
        {
            db = new OnlineShopDbContext();
        }

        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public List<Product> ListFeature(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot >= DateTime.Now).OrderByDescending(x => x.CreateDate).Take(top).ToList();
        }

        public PagedResult<ProductViewModel> LoadData(string searchString, int page, int pageSize)
        {
            var model = from a in db.Products
                        join b in db.Categories
                        on a.CategoryID equals b.ID
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Image = a.Image,
                            Name = a.Name,
                            CateName = b.Name,
                            CateMetaTitle = b.MetaTitle,
                            MetaTitle = a.MetaTitle,
                            Price = a.Price,
                            CreateDate = a.CreateDate
                        };
            return model.OrderByDescending(x=>x.ID).ExecutePagingResult(page, pageSize);
        }

        public int Insert(Product product)
        {
            product.MetaTitle = Utility.ConvertToUnSign(product.Name);
            product.CreateDate = DateTime.Now;
            product.Status = false;
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }

        public List<ProductViewModel> ListAllProduct(ref int totalRecord, int pageIndex, int pageSize)
		{
			//var model = db.Products.Where(x => x.Status == false && x.CategoryID == id);
			var model = from a in db.Products
						join b in db.Categories
						on a.CategoryID equals b.ID
						select new ProductViewModel()
						{
							ID = a.ID,
							Image = a.Image,
							Name = a.Name,
							CateName = b.Name,
							CateMetaTitle = b.MetaTitle,
							MetaTitle = a.MetaTitle,
							Price = a.Price,
							CreateDate = a.CreateDate
						};
			totalRecord = model.Count();
			return model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
		}


        public List<ProductViewModel> ListByCategoryId(int id, ref int totalRecord, int pageIndex, int pageSize)
        {
            //var model = db.Products.Where(x => x.Status == false && x.CategoryID == id);
            var model = from a in db.Products
						join b in db.Categories
                        on a.CategoryID equals b.ID
                        where a.CategoryID == id
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Image = a.Image,
                            Name = a.Name,
                            CateName = b.Name,
                            CateMetaTitle = b.MetaTitle,
                            MetaTitle = a.MetaTitle,
                            Price = a.Price,
                            CreateDate = a.CreateDate
                        };
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex, int pageSize)
        {
            var model = from a in db.Products
						join b in db.Categories
                        on a.CategoryID equals b.ID
                        where a.Name.Contains(keyword)
                        select new ProductViewModel()
                        {
                            ID = a.ID,
                            Image = a.Image,
                            Name = a.Name,
                            CateName = b.Name,
                            CateMetaTitle = b.MetaTitle,
                            MetaTitle = a.MetaTitle,
                            Price = a.Price,
                            CreateDate = a.CreateDate
                        };
            totalRecord = model.Count();
            return model.OrderByDescending(x => x.CreateDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Delete(int id)
        {
            var entity = db.Products.Find(id);
            db.Products.Remove(entity);
            db.SaveChanges();
        }

        public int Update(Product product)
        {
            try
            {
                var entity = db.Products.Find(product.ID);
                entity.Name = product.Name;
                entity.MetaTitle = Utility.ConvertToUnSign(entity.Name);
                entity.Description = product.Description;
                entity.Price = product.Price;
                entity.CategoryID = product.CategoryID;
                entity.Detail = product.Detail;
                entity.Waranty = product.Waranty;
                entity.ModifileDate = DateTime.Now;
                entity.TopHot = product.TopHot;
                db.SaveChanges();

                return entity.ID;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public Product ViewDetail(int id)
        {
            return db.Products.Find(id);
        }

        public List<Product> GetRelatedProduct(int id)
        {
            var detail = db.Products.Find(id);
            return db.Products.Where(x => x.CategoryID == detail.CategoryID).Take(4).ToList();
        }

        public List<string> SearchIndex(string keyWord)
        {
            var model = db.Products.Where(x => x.Name.Contains(keyWord)).Select(x=>x.Name);
            return model.ToList();
        }
    }
}

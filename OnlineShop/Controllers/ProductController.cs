using Model.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class ProductController : Controller
    {
		public int pageSize = 8;
        //
        // GET: /Product/
		public ActionResult Index(int page = 1)
        {
			int totalRecord = 0;
			var listCategory = new ProductService().ListAllProduct(ref totalRecord, page, pageSize);

			ViewBag.Total = totalRecord;
			ViewBag.Page = page;

			int maxPage = 5;
			int totalPage = 0;

			totalPage = (int)Math.Ceiling((double)((double)totalRecord / pageSize));
            var toa = Math.Ceiling((double)((double)totalRecord / pageSize));

            ViewBag.TotalPage = totalPage;
			ViewBag.MaxPage = maxPage;
			ViewBag.LastPgae = totalPage;
			ViewBag.NextPage = page + 1;
			ViewBag.PrivPage = page - 1;


			return View(listCategory);
        }

        [ChildActionOnly]
        public ActionResult ProductCategory()
        {
			var model = new CategoryService().ListAll();
            return PartialView(model);
        }

        public ActionResult Category(int id, int page = 1)
        {
            var entity = new CategoryService().ViewDetail(id);
            ViewBag.Category = entity;

            int totalRecord = 0;
            var listCategory = new ProductService().ListByCategoryId(id, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.LastPgae = totalPage;
            ViewBag.NextPage = page + 1;
            ViewBag.PrivPage = page - 1;


            return View(listCategory);
        }

        public ActionResult Search(string keyword, int page = 1)
        {
            int totalRecord = 0;
            var listCategory = new ProductService().Search(keyword, ref totalRecord, page, pageSize);

            ViewBag.Total = totalRecord;
            ViewBag.Page = page;
            ViewBag.KeyWord = keyword;

            int maxPage = 5;
            int totalPage = 0;

            totalPage = (int)Math.Ceiling((double)(totalRecord / pageSize));
            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.LastPgae = totalPage;
            ViewBag.NextPage = page + 1;
            ViewBag.PrivPage = page - 1;


            return View(listCategory);
        }

		//[OutputCache(CacheProfile = "Cache1DayForProduct")]
        public ActionResult Detail(int id)
        {
            var entity = new ProductService().ViewDetail(id);
            ViewBag.RelatedProduct = new ProductService().GetRelatedProduct(id);
            return View(entity);
        }

        public JsonResult SearchIndex(string q)
        {
            var data = new ProductService().SearchIndex(q);
            return Json(new
            {
                data= data,
                status= true
            },JsonRequestBehavior.AllowGet);
        }
	}
}
using Model.Dao;
using Model.EF;
using Model.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : Controller
    {
        //
        // GET: /Admin/Content/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            //OnlineShopDbContext db = new OnlineShopDbContext();
            //ViewBag.CategorySi = new SelectList(db.Categories, "ID", "Name");
            SetViewBag();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content content)
        {
            return View();
        }

        public void SetViewBag(long? selectedID = null)
        {
            CategoryService dao = new CategoryService();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedID);
        }
	}
}
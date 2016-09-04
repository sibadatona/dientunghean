using Entity.View;
using Model.Dao;
using Model.Service;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDao().ListAll();
            var model = new ProductService();
            ViewBag.ListNewProduct = model.ListNewProduct(4);
            ViewBag.ListFeature = model.ListFeature(4);
            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration=3600)]
        public ActionResult MainMenu()
        {
            var model = new MenuDao().ListByGroupID("menuchinh");
            return PartialView(model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult MenuTop()
        {
            var model = new MenuDao().ListByGroupID("menutop");
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult HeaderCart()
        {
            var cart = Session[CommonConstant.CART_SESSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return PartialView(list);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult Footer()
        {
            var model = new FooterDao().GetFooter();
            return PartialView(model);
        }
    }
}
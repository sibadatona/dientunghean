using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entity.View;
using Model.Dao;
using OnlineShop.Common;
using System.Web.Script.Serialization;
using Model.EF;
using Common;
using Model.Service;

namespace OnlineShop.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
        public ActionResult Index()
        {
            var cart = Session[CommonConstant.CART_SESSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }

        public ActionResult AddCart(int productId, int quantity)
        {
            var product = new ProductService().ViewDetail(productId);
            var cart = Session[CommonConstant.CART_SESSION];
            if (cart != null)
            {
                var list = (List<CartItem>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    //create new cart
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[CommonConstant.CART_SESSION] = list;
            }
            else
            {
                //create new cart
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                Session[CommonConstant.CART_SESSION] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart = (List<CartItem>)Session[CommonConstant.CART_SESSION];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            Session[CommonConstant.CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CommonConstant.CART_SESSION] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteItem(int productId)
        {
            var sessionCart = (List<CartItem>)Session[CommonConstant.CART_SESSION];
            sessionCart.RemoveAll(x => x.Product.ID == productId);
            Session[CommonConstant.CART_SESSION] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public ActionResult Payment()
        {
            var cart = Session[CommonConstant.CART_SESSION];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }

            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(string shipName, string mobile, string address, string email)
        {
            var model = new CartDao();
            Order order = new Order();
            order.ShipName = shipName;
            order.ShipMobile = mobile;
            order.ShipAddress = address;
            order.ShipEmail = email;
            order.CreateDate = DateTime.Now;
            int id = model.InsertOrder(order);
            var sessionCart = (List<CartItem>)Session[CommonConstant.CART_SESSION];
            decimal total = 0;

            foreach (var item in sessionCart)
            {
                OrderDetail orderDetail = new OrderDetail();
                orderDetail.ProducID = item.Product.ID;
                orderDetail.OrderID = id;
                orderDetail.Quantity = item.Quantity;
                orderDetail.Price = item.Product.Price;
                model.InsertOrderDetail(orderDetail);

                total += (item.Product.Price.GetValueOrDefault(0) * item.Quantity);
            }

            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/client/template/newOrder.html"));

            content = content.Replace("{{CustomerName}}", shipName);
            content = content.Replace("{{Phone}}", mobile);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);
            content = content.Replace("{{Total}}", total.ToString("N0"));

            new MailHelper().SendMail(email, "Đơn hàng từ Online Shop", content);

            return Redirect("/hoan-thanh");
        }

        public ActionResult Success()
        {
            return View();
        }
    }
}
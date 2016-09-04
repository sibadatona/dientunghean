using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace OnlineShop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // BotDetect requests must not be routed
            routes.IgnoreRoute("{*botdetect}",
              new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

			//San pham
			routes.MapRoute(
				name: "Products",
				url: "san-pham",
				defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
				namespaces: new[] { "OnlineShop.Controllers" }
			);

            //San pham
            routes.MapRoute(
                name: "Category",
                url: "san-pham/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Category", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Chi tiet san pham
            routes.MapRoute(
                name: "Produc detail",
                url: "chi-tiet/{metatitle}-{id}",
                defaults: new { controller = "Product", action = "Detail", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Gioi thieu
            routes.MapRoute(
                name: "About",
                url: "gioi-thieu",
                defaults: new { controller = "About", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Them vao gio hang
            routes.MapRoute(
                name: "Add cart",
                url: "them-gio-hang",
                defaults: new { controller = "Cart", action = "AddCart", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //gio hang
            routes.MapRoute(
                name: "Cart",
                url: "gio-hang",
                defaults: new { controller = "Cart", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //thanh toan
            routes.MapRoute(
                name: "Payment",
                url: "thanh-toan",
                defaults: new { controller = "Cart", action = "Payment", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //hoan thanh
            routes.MapRoute(
                name: "Success",
                url: "hoan-thanh",
                defaults: new { controller = "Cart", action = "Success", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //hoan thanh
            routes.MapRoute(
                name: "Contact",
                url: "lien-he",
                defaults: new { controller = "Contact", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Dang ky user
            routes.MapRoute(
                name: "Register",
                url: "dang-ky",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Dang nhap use
            routes.MapRoute(
                name: "Login",
                url: "dang-nhap",
                defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Tim kiem chung
            routes.MapRoute(
                name: "Search Index",
                url: "tim-kiem-chung",
                defaults: new { controller = "Product", action = "Search", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers" }
            );

            //Default
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "OnlineShop.Controllers"}
            );
        }
    }
}

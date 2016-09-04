using Entity.View;
using Model.Dao;
using Model.Service;
using OnlineShop.Common;
using OnlineShop.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/
        public ActionResult Index()
        {
            if(System.Web.HttpContext.Current.Session[CommonConstant.ADMIN_USER_SESSION] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            UserService userDao = new UserService();
            if (ModelState.IsValid)
            {
                var result = userDao.Login(loginModel.UserName, loginModel.Password);
                if (result)
                {
                    var userID = userDao.GetID(loginModel.UserName);
                    var userSession = new UserLogin();
                    userSession.UserID = userID.ID;
                    userSession.UserName = loginModel.UserName;
                    userSession.Role = userID.Role;
                    Session.Add(CommonConstant.ADMIN_USER_SESSION, userSession);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "UserName hoặc mật khẩu không đúng!");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[CommonConstant.ADMIN_USER_SESSION] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}
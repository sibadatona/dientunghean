using Entity.View;
using Model.Dao;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.UI.Mvc;
using OnlineShop.Common;
using System.Xml.Linq;
using Model.ViewModel;
using Model.Service;

namespace OnlineShop.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode", "RegisterCaptcha", "Sai CAPTCHA code!")]
        public ActionResult Register(Account account)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.UserName = account.UserName;
                user.Password = account.Password;
                user.Name = account.Name;
                user.Phone = account.Phone;
                user.Address = account.Address;
                user.Email = account.Email;
                user.Province = account.Province;
                user.District = account.District;
                user.Village = account.Viilage;
                int temp = new UserService().Insert(user);
                if (temp > 0)
                {
                    ViewBag.Success = "Đăng ký thành công!";
                }
                else
                {
                    ModelState.AddModelError("", "Đăng ký thất bại!");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLogin login)
        {
            if (ModelState.IsValid)
            {
                UserService userDao = new UserService();
                if (userDao.Login(login.UserName, login.Password))
                {
                    Session[CommonConstant.USER_SESSION] = login;

                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session[CommonConstant.USER_SESSION] = null;
            return Redirect("/");
        }

        public JsonResult LoadProvinces()
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_VietNam.xml"));

            var xElements = xmlDoc.Element("Root").Elements("Item").Where(x => x.Attribute("type").Value == "province");
            var list = new List<ProvinceModel>();
            ProvinceModel province = null;
            foreach (var item in xElements)
            {
                province = new ProvinceModel();
                province.Id = int.Parse(item.Attribute("id").Value);
                province.Name = item.Attribute("value").Value;
                list.Add(province);
            }


            return Json(new
            {
                data = list,
                status = true
            });
        }

        public JsonResult LoadDistrict(int provinceId)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_VietNam.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item").
                Where(x => x.Attribute("type").Value == "province" && int.Parse(x.Attribute("id").Value) == provinceId);

            var list = new List<DistrictModel>();
            DistrictModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "district"))
            {
                district = new DistrictModel();
                district.Id = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.ProvinceID = provinceId;
                list.Add(district);
            }

            return Json(new
            {
                data = list,
                status = true
            });
        }

        public JsonResult LoadVillage(int districtID)
        {
            var xmlDoc = XDocument.Load(Server.MapPath(@"~/Assets/client/data/Provinces_VietNam.xml"));

            var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item").
              Where(x => x.Attribute("type").Value == "district" && int.Parse(x.Attribute("id").Value) == districtID);

            //var xElement = xmlDoc.Element("Root").Elements("Item").Elements("Item").Elements("Item").
            //    Where(x => x.Attribute("type").Value == "precinct" && int.Parse(x.Attribute("id").Value) == districtID);

            var list = new List<VillageModel>();
            VillageModel district = null;
            foreach (var item in xElement.Elements("Item").Where(x => x.Attribute("type").Value == "precinct"))
            {
                district = new VillageModel();
                district.Id = int.Parse(item.Attribute("id").Value);
                district.Name = item.Attribute("value").Value;
                district.DistrictID = districtID;
                list.Add(district);
            }

            return Json(new
            {
                data = list,
                status = true
            });
        }
    }
}
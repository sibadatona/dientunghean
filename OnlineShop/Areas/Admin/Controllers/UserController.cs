using Model.Service;
using Model.EF;
using OnlineShop.Controllers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Model;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private UserService _user = null;

        public UserController()
        {
            _user = new UserService();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string searchString, string staus, int page, int pageSize = 4)
        {
            PagedResult<User> paging = _user.LoadData(searchString, staus, page, pageSize);
            return Json(new
            {
                data = paging.Items,
                total = paging.TotalCount,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if(ModelState.IsValid)
            {
                var dao = new UserService();
                int temp = dao.Insert(user);
                if(temp > 0)
                {
                    SetAlert("Thêm mới thành công.", "success");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm mới user thất bại!");
                }
            }
            return View();
        }

        [HttpGet]
        public JsonResult Detail(int id)
        {
            var dao = _user.ViewDetail(id);
            return Json(new
            {
                data = dao,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Update(string model)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            User user = serializer.Deserialize<User>(model);

            var dao = _user.Update(user);

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool check = _user.Delete(id);

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
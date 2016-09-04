using Model.EF;
using Model.Service;
using OnlineShop.Controllers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class CategoryController : BaseController
    {
        private readonly CategoryService _categoyService = null;
        public CategoryController()
        {
            _categoyService = new CategoryService();
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            //var items = _categoyService.ListAll();
            return View();
        }

        [HttpGet]
        public JsonResult LoadData()
        {
            var items = _categoyService.ListAll();
            return Json(new {
                data=items,
                status = true
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AddOrUpdate(string model)
        {
            JavaScriptSerializer serial = new JavaScriptSerializer();
            Category cate = serial.Deserialize<Category>(model);

            if (cate.ID == 0)
            {
                _categoyService.Insert(cate);
            }
            else
            {
                _categoyService.Update(cate);
            }

            return Json(new {
                status= true
            },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Delete(int Id)
        {
            _categoyService.Delete(Id);
            return Json(new {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
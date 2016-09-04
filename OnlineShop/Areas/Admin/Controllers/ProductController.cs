using Model;
using Model.EF;
using Model.Service;
using Model.ViewModel;
using OnlineShop.Controllers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductService _producService = null;
        private readonly CategoryService _categoryService = null;

        public ProductController()
        {
            _producService = new ProductService();
            _categoryService = new CategoryService();
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult LoadData(string searchString, int page, int pageSize = 4)
        {
            PagedResult<ProductViewModel> paging = _producService.LoadData(searchString, page, pageSize);

            return Json(new
            {
                data = paging.Items.ToList(),
                total = paging.TotalCount,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var categories = _categoryService.ListAll();
            ViewBag.Categories = new SelectList(categories, "ID", "Name");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {

                if (file != null)
                {
                    try
                    {
                        string pic = Guid.NewGuid().ToString("N") + System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(
                                               Server.MapPath("~/Content/Upload/images/"), pic);

                        file.SaveAs(path);

                        product.Image = "/Content/Upload/images/" + pic;

                        int temp = _producService.Insert(product);

                        if (temp > 0)
                        {
                            SetAlert("Thêm mới thành công.", "success");
                            return RedirectToAction("Index", "Product");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Thêm mới user thất bại!");
                        }
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("", "Thêm mới user thất bại!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Vui lòng chọn hình ảnh cho sản phẩm!");
                }
            }

            var categories = _categoryService.ListAll();
            ViewBag.Categories = new SelectList(categories, "ID", "Name");

            return View();
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            var product = _producService.ViewDetail(id);
            var categories = _categoryService.ListAll();
            ViewBag.Categories = new SelectList(categories, "ID", "Name", product.CategoryID);

            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                int temp = _producService.Update(product);
                if (temp > 0)
                {
                    SetAlert("Chỉnh sửa thành công.", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Chỉnh sửa user thất bại!");
                }

            }
            return View();
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            _producService.Delete(id);

            return Json(new
            {
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
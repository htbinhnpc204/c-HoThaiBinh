using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class QLDanhMucController : Controller
    {
        // GET: Admin/QLDanhMuc
        HoThaiBinhDbContext db = new HoThaiBinhDbContext();
        int pageSize = 5;

        IPagedList<tblCategory> model;
        public ActionResult Index(int? page)
        {
            int pageNum = page ?? 1;
            var session = Code.SessionHelper.getSession();
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            model = db.tblCategories.OrderBy(p => p.categoryID).ToPagedList(pageNum, pageSize);
            return View(model);
        }
        public ActionResult suaDM(int? id)
        {
            var session = Code.SessionHelper.getSession();
            if (id == null)
                return RedirectToAction("Index");
            var sp = (from s in db.tblCategories where s.categoryID == (id ?? 1) select s).FirstOrDefault();
            return View(sp);
        }

        [HttpPost]
        public ActionResult SuaDM(tblCategory prod)
        {
            if (ModelState != null)
            {
                tblCategory s = db.tblCategories.FirstOrDefault(b => b.categoryID == prod.categoryID);
                if (s != null)
                {
                    s.categoryName = prod.categoryName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao = "Không thể chỉnh sửa";
            return Redirect("Index");
        }
        public ActionResult XoaDM(int? id)
        {
            var session = Code.SessionHelper.getSession();
            if (id == null)
            {
                ViewBag.ThongBao = "Không tìm thấy id";
                return Redirect("Index");
            }
                
            var sp = (from s in db.tblCategories where s.categoryID == (id ?? 1) select s).FirstOrDefault();
            db.tblCategories.Remove(sp);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa thành công";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult ThemDM(tblCategory prod)
        {
            if (ModelState != null)
            {
                tblCategory s = new tblCategory();
                
                    s.categoryName = prod.categoryName;
                db.tblCategories.Add(s);
                    db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ThongBao = "Thêm thành công";
            return View();
        }
    }
}
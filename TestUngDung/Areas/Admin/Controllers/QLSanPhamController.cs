using ModelEF.Model;
using PagedList;
using System.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web;
using System.IO;
using TestUngDung.Areas.Admin.Code;
using System.Drawing;

namespace TestUngDung.Areas.Admin.Controllers
{
    
    public class QLSanPhamController : Controller
    {

        HoThaiBinhDbContext db = new HoThaiBinhDbContext();
        int pageSize = 5;

        IPagedList<tblProduct> model;

        public ActionResult Index(int? page)
        {
            int pageNum = page ?? 1;
            var session = Code.SessionHelper.getSession(); 
            if (session == null)
            {
                return RedirectToAction("","Login",null);
            }
            model = db.tblProducts.OrderBy(p => p.prodID).ToPagedList(pageNum, pageSize);
            return View(model);
        }
        public ActionResult suaSP(int? id)
        { 
            var session = Code.SessionHelper.getSession();
            if (id == null)
                return RedirectToAction("Index");
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            ViewBag.DanhMuc = new SelectList(db.tblCategories.ToList().OrderBy(c => c.categoryID), "categoryID", "categoryName");
            var sp = (from s in db.tblProducts where s.prodID == (id ?? 1) select s).FirstOrDefault();
            return View(sp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult SuaSP(tblProduct prod, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.State = 1;
            }
            if (ModelState != null)
            {
                ViewBag.DanhMuc = new SelectList(db.tblCategories.ToList().OrderBy(c => c.categoryID), "categoryID", "categoryName");
                tblProduct s = db.tblProducts.FirstOrDefault(b => b.prodID == prod.prodID);
                if (s != null)
                {
                    byte[] imageByte = null;
                    if (fileUpload != null)
                    {
                        BinaryReader rdr = new BinaryReader(fileUpload.InputStream);
                        imageByte = rdr.ReadBytes((int)fileUpload.ContentLength);
                    }
                    s.prodImage = imageByte;
                    s.prodName = prod.prodName;
                    s.prodDescription = prod.prodDescription;
                    s.produnitCost = prod.produnitCost;
                    s.prodQuantity = prod.prodQuantity;
                    s.categoryID = prod.categoryID;
                    s.prodStatus = prod.prodStatus;
                    db.SaveChanges();
                }
                ViewBag.ThongBao = "Sửa thành công sản phẩm: " +s.prodID;
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult themSP()
        {
            var session = SessionHelper.getSession();
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            ViewBag.DanhMuc = new SelectList(db.tblCategories.ToList().OrderBy(c => c.categoryID), "categoryID", "categoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult themSP(tblProduct prod, HttpPostedFileBase fileUpload)
        {
            if (fileUpload == null)
            {
                ViewBag.State = 1;
            }
            if (ModelState != null)
            {
                ViewBag.DanhMuc = new SelectList(db.tblCategories.ToList().OrderBy(c => c.categoryID), "categoryID", "categoryName");
                tblProduct s = new tblProduct();
                byte[] imageByte = null;
                if (fileUpload != null)
                {
                    BinaryReader rdr = new BinaryReader(fileUpload.InputStream);
                    imageByte = rdr.ReadBytes((int)fileUpload.ContentLength);
                }
                s.prodName = prod.prodName;
                s.prodDescription = prod.prodDescription;
                    s.produnitCost = prod.produnitCost;
                    s.prodQuantity = prod.prodQuantity;
                    s.categoryID = prod.categoryID;
                s.prodImage = imageByte;
                s.prodStatus = true;
                db.tblProducts.Add(s);
                db.SaveChanges();
                ViewBag.ThongBao = "Thêm sản phẩm " + s.prodName + " thành công!";
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult XoaSP(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            tblProduct sp = (from s in db.tblProducts where s.prodID == (id ?? 1) select s).FirstOrDefault();
            db.tblProducts.Remove(sp);
            db.SaveChanges();
            ViewBag.ThongBao = "Xóa thành công sản phẩm: " + id;
            return RedirectToAction("Index");
        }

        public ActionResult chiTietSP(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");
            if (Session["Admin"] == null)
            {
                return RedirectToAction("Login");
            }
            ViewBag.DanhMuc = new SelectList(db.tblCategories.ToList().OrderBy(c => c.categoryID), "categoryID", "categoryName");
            tblProduct sp = (from s in db.tblProducts where s.prodID == (id ?? 1) select s).FirstOrDefault();
            return View(sp);
        }
    }
}
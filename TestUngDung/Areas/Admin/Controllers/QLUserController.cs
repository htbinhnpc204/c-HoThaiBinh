using ModelEF.Model;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Models;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class QLUserController : Controller
    {
        // GET: Admin/QLUser
        HoThaiBinhDbContext db = new HoThaiBinhDbContext();
        int pageSize = 5;

        IPagedList<tblUserAccount> model;

        public ActionResult Index(int? page)
        {
            int pageNum = page ?? 1;
            var session = Code.SessionHelper.getSession();
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            model = db.tblUserAccounts.OrderBy(u => u.ID).ToPagedList(pageNum, pageSize);
            return View(model);
        }
        public ActionResult suaUser(int? id)
        {
            var session = Code.SessionHelper.getSession();
            if (id == null)
                return RedirectToAction("Index");
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            var select = (from s in db.tblUserAccounts where s.ID == (id ?? 1) select s).FirstOrDefault();
            return View(select);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SuaUser(tblUserAccount user)
        {
            if (ModelState != null)
            {
                tblUserAccount s = db.tblUserAccounts.FirstOrDefault(b => b.ID == user.ID);
                if (s != null)
                {
                    s.userName = user.userName;
                    s.passWord = user.passWord;
                    s.status = user.status;
                    s.displayName = user.displayName;
                    db.SaveChanges();
                    ViewBag.ThongBao = "Chỉnh sửa thành công";
                    return RedirectToAction("Index");
                
                }
                
            }
            ViewBag.ThongBao = "Chỉnh sửa thất bại";
            return View(user);
        }
        public ActionResult blockUser(int? id)
        {
            var session = Code.SessionHelper.getSession();
            if (id == null)
                return RedirectToAction("Index");
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            var select = (from s in db.tblUserAccounts where s.ID == (id ?? 1) select s).FirstOrDefault();
            select.status = false;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult xoaUser(int? id)
        {
            var session = Code.SessionHelper.getSession();
            if (id == null)
                return RedirectToAction("Index");
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            var select = (from s in db.tblUserAccounts where s.ID == (id ?? 1) select s).FirstOrDefault();
            db.tblUserAccounts.Remove(select);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult chiTietUser(int? id)
        {
            var session = Code.SessionHelper.getSession();
            if (id == null)
                return RedirectToAction("Index");
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            var select = (from s in db.tblUserAccounts where s.ID == (id ?? 1) select s).FirstOrDefault();
            return View(select);
        }

        public ActionResult themUser()
        {
            var session = Code.SessionHelper.getSession();
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemUser(tblUserAccount user)
        {
            if (ModelState != null)
            {
                tblUserAccount s = new tblUserAccount();
               
                s.userName = user.userName;
                s.passWord = user.passWord;
                s.status = user.status;
                s.displayName = user.displayName;
                try
                {
                    db.tblUserAccounts.Add(s);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.ThongBao = "Tài khoản đã tồn tại";
                    return View();
                }
            }
            return View(user);
        }
    }
}
using ModelEF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Code;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        HoThaiBinhDbContext db = new HoThaiBinhDbContext();
        // GET: Admin/Home
        public ActionResult Index()
        {
            var session = Code.SessionHelper.getSession();
            if (session == null)
            {
                return RedirectToAction("", "Login", null);
            }
            var user = db.tblUserAccounts.Where(m => m.userName == session.userName).FirstOrDefault();
            return View(user);
        }
    }
}
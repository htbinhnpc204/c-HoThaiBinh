using ModelEF.Model;
using System.Linq;
using System.Web.Mvc;
using TestUngDung.Areas.Admin.Models;
using TestUngDung.Areas.Admin.Code;

namespace TestUngDung.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        HoThaiBinhDbContext db = new HoThaiBinhDbContext();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = db.tblUserAccounts.Where(u=> u.userName == model.UserName && u.passWord == model.Password).FirstOrDefault();
                if (result == null || !result.status)
                {
                    ModelState.AddModelError("","Đăng nhập thất bại");
                    return View("Index");
                }
                SessionHelper.setSession(new UserSession() { userName = model.UserName });
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceProject.Models;
using EcommerceProject.Models.ViewModel;

namespace EcommerceProject.Controllers
{
    public class AdminUserController : Controller
    {
        // GET: AdminUser
        KathfordDBEntities _db = new KathfordDBEntities();
        public ActionResult ManageUser()
        {
            return View();
        }
        public JsonResult GetData()
        {
            using (KathfordDBEntities db = new KathfordDBEntities())
            {
                db.Configuration.LazyLoadingEnabled = false;
                List<UserViewModel> lstitem = new List<UserViewModel>();
                var lst = db.tblUsers.ToList();
                foreach (var item in lst)
                {
                    lstitem.Add(new UserViewModel() { UserId = item.UserId, Username = item.Username});
                }
                return Json(new { data = lstitem }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult AddOrEdit()
        {
            return View();
        }
        [HttpPost]

        public ActionResult AddOrEdit(UserViewModel uv)
        {
            tblUser tb = new tblUser();
            tb.Username = uv.Username;
            tb.Password = uv.Password;

            //HttpPostedFileBase fup = Request.Files["Photo"];
            //if (fup != null)
            //{
            //    if (fup.FileName != "")
            //    {
            //        tb.photo = fup.FileName;
            //        fup.SaveAs(Server.MapPath("~/images/UserImages/" + fup.FileName));
            //    }
            //}
            _db.tblUsers.Add(tb);
            _db.SaveChanges();

            UserRole ud = new UserRole();
            ud.UserId = tb.UserId;
            ud.UserRoleId= 1;
            _db.UserRoles.Add(ud);
            _db.SaveChanges();
            ViewBag.Message = "User Created Successfully";


            return View();
        }
        [HttpPost]

        public ActionResult Delete(int id)
        {
            using (KathfordDBEntities db = new KathfordDBEntities())
            {
                tblUser sm = db.tblUsers.Where(x => x.UserId == id).FirstOrDefault();
                db.tblUsers.Remove(sm);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
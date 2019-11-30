using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using EcommerceProject.Models;
using EcommerceProject.Models.ViewModel;

namespace EcommerceProject.Controllers
{
    public class AccountController : Controller
    {


        KathfordDBEntities db = new KathfordDBEntities();

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(LoginViewModel l, string ReturnUrl = "")
        {
            using (KathfordDBEntities db = new KathfordDBEntities())
            {
                var users = db.tblUsers.Where(a => a.Username == l.Username && a.Password == l.Password).FirstOrDefault();
                if (users != null)
                {
                     //Session.Add("img", users.Photo);
                    Session.Add("username", users.Username);
                    FormsAuthentication.SetAuthCookie(l.Username, true);
                    FormsAuthentication.SetAuthCookie(l.Username, l.RememberMe);
                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        Session.Add("userid", users.UserId);
                        if (users.UserRoles.Where(r => r.RoleId == 1).FirstOrDefault() != null)
                        {
                            return RedirectToAction("Dashboard", "Admin");

                        }

                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }

                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid User");
                }
            }
            return View();

        }
        [Authorize]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult ChangePassword()
        {



            return View();
        }
        [HttpPost]

        public ActionResult ChangePassword(ChangePasswordViewModel ch)
        {
            int userid = Convert.ToInt32(Session["userid"].ToString());

            tblUser us = db.tblUsers.Where(u => u.UserId == userid && u.Password == ch.OldPassword).FirstOrDefault();
            if (us != null)
            {
                us.Password = ch.NewPassword;
                db.SaveChanges();

            }
            else
            {
                ViewBag.Message = "Wrong Old Password";
            }
            return Json(new { success = true, message = "Password Changed Successfully" }, JsonRequestBehavior.AllowGet);
        }

    }

}

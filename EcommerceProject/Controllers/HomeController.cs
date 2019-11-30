using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceProject.Models;
using EcommerceProject.Models.ViewModel;
using PagedList;

namespace EcommerceProject.Controllers
{
    public class HomeController : Controller
    {
        KathfordDBEntities db = new KathfordDBEntities();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserViewModel uv)
        {
            tblUser tbl = db.tblUsers.Where(u => u.Username == uv.Username).FirstOrDefault();
            if (tbl != null)
            {
                return Json(new { success = false, message = "User Already Register" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                tblUser tb = new tblUser();
                tb.Username = uv.Username;
                tb.Password = uv.Password;
                db.tblUsers.Add(tb);
                db.SaveChanges();

                UserRole ud = new UserRole();
                ud.UserId = tb.UserId;
                ud.UserRoleId = 2;
                db.UserRoles.Add(ud);
                db.SaveChanges();
                return Json(new { success = true, message = "User Register Successfully" }, JsonRequestBehavior.AllowGet);
            }
        }


            public ActionResult ProductList(string search, int? page, int id = 0)
            {

                if (id != 0)
                {

                    return View(db.tblProducts.Where(p => p.CategoryId == id).ToList().ToPagedList(page ?? 1, 4));
                }
                else
                {
                    if (search != "")
                    {
                        return View(db.tblProducts.Where(x => x.Description.Contains(search) || x.ProductName.Contains(search) || search == null).ToList().ToPagedList(page ?? 1, 4));
                    }
                    else
                    {
                        return View(db.tblProducts.ToList().ToPagedList(page ?? 1, 4));
                    }

                }

            }

        //public ActionResult ForgetPassword(UserViewModel uv)
        //{

        //    if (ModelState.IsValid)
        //    {
        //        //https://www.google.com/settings/security/lesssecureapps
        //        //Make Access for less secure apps=true

        //        string from = "gfccafe123@gmail.com";
        //        using (MailMessage mail = new MailMessage(from, uv.Username))
        //        {
        //            try
        //            {
        //                tblUser tb = _db.tblUsers.Where(u => u.Username == uv.Username).FirstOrDefault();
        //                if (tb != null)
        //                {
        //                    mail.Subject = "Password Recovery";
        //                    mail.Body = "Your Password is:" + tb.Password;

        //                    mail.IsBodyHtml = false;
        //                    SmtpClient smtp = new SmtpClient();
        //                    smtp.Host = "smtp.gmail.com";
        //                    smtp.EnableSsl = true;
        //                    NetworkCredential networkCredential = new NetworkCredential(from, "suneel!@#123");
        //                    smtp.UseDefaultCredentials = false;
        //                    smtp.Credentials = networkCredential;
        //                    smtp.Port = 587;
        //                    smtp.Send(mail);
        //                    ViewBag.Message = "Your Password Is Sent to your email";
        //                }
        //                else
        //                {
        //                    ViewBag.Message = "email Doesnot Exist in Database";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            finally
        //            {

        //            }

        //        }

        //    }
        //    return View();


        }
}
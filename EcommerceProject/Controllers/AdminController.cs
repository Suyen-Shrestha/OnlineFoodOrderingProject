using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceProject.Models;

namespace EcommerceProject.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        KathfordDBEntities db = new KathfordDBEntities();
        [Authorize(Roles = "Admin")]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
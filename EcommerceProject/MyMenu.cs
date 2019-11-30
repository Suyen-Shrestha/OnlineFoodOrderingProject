using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceProject.Models
{
    public class MyMenu
    {
        public static List<tblCategory> GetMenus()
        {
            using (var context = new KathfordDBEntities())
            {
                return context.tblCategories.ToList();
            }
        }
      
    }
}
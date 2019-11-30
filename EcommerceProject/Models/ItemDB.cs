using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceProject.Models
{
    public static class ItemDB
    {
        public static List<tblProduct> GetAllSpecialItem()
        {
            using (var context = new KathfordDBEntities())
            {
                return context.tblProducts.OrderByDescending(e => e.ProductId).Where(s => s.IsSpecial == true).Take(4).ToList();
            }
        }
        public static List<tblProduct> GetAllItems()
        {
            using (var context = new KathfordDBEntities())
            {
                return context.tblProducts.Take(8).ToList();
            }
        }
    }
}
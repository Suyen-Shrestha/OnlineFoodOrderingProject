using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EcommerceProject.Models.ViewModel
{
    public class LoginViewModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Usename Required")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
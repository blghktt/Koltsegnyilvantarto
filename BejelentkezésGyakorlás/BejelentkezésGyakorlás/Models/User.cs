using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BejelentkezésGyakorlás.Models
{
    public class User
    {
        [Display(Name="Felhasználónév")]
        public string UserName { get; set; }

        [Display(Name ="Jelszó")]
        public string Password { get; set; }
    }
}
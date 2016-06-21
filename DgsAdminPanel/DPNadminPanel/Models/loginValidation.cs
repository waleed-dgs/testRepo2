using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace DPNadminPanel.Models
{
    public class loginValidation
    {

        [Required]
        [Display(Name = "User Login")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
}
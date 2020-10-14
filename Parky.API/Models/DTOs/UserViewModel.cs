using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Parky.API.Models.DTOs
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="Please Enter User Name")]
        public string UserName { get; set; }


        [Required]
        public string UserPassword { get; set; }
    }

}

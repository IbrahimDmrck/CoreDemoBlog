using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Models
{
    public class UserUpdateViewModel
    {
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string ImageUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

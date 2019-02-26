using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneDirectory.Model.ViewModel
{
    public class VMAreaAccountLogin
    {
        [Required(ErrorMessage = "Email address is required")]
        [StringLength(50, ErrorMessage = "Must be max. 50 characters")]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(18, ErrorMessage = "Password must be between 5 and 18 characters", MinimumLength = 5)]
        public string Password { get; set; }
    }
}

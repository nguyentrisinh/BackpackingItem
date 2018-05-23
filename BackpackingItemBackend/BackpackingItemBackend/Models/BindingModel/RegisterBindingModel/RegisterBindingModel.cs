using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.BindingModel.RegisterBindingModel
{
    public class RegisterBindingModel
    {
        [EmailAddress(ErrorMessage = "2002")]
        public string Email { get; set; } // Also username

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

    }
}

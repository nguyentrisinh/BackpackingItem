using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class ApplicationUserReturnModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public Status Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public Role Role { get; set; }

        public static ApplicationUserReturnModel Create(ApplicationUser applicationUser)
        {
            return new ApplicationUserReturnModel
            {
                Id = applicationUser.Id,
                Email = applicationUser.Email,
                Username = applicationUser.UserName,
                FirstName = applicationUser.FirstName,
                LastName = applicationUser.LastName,
                Gender = applicationUser.Gender,
                Status = applicationUser.Status,
                Birthday = applicationUser.Birthday,
                Role = applicationUser.Role
            };
        }
    }
}

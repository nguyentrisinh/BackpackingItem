using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace BackpackingItemBackend.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public Status Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }


        public Role Role { get; set; }

        #region CustomerOrders
        public List<Order> CustomerOrders { get; set; }
        #endregion

        #region ShipmentInfos
        public List<ShipmentInfo> ShipmentInfos { get; set; }
        #endregion

    }

    public enum Role
    {
        Customer = 1,
        Admin = 2
    }

    public enum Status
    {
        Active = 1,
        Inactive = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}

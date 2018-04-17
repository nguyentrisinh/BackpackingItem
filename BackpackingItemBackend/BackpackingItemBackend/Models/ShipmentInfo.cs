using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class ShipmentInfo
    {
        #region Id 
        public long Id { get; set; }
        #endregion

        #region Phone 
        public string Phone { get; set; }
        #endregion

        #region ReceivedPersonName 
        public string ReceivedPersonName { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        #endregion

        #region Customer
        public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        #endregion

        #region District
        public District District { get; set; }
        public long DistrictId { get; set; }
        #endregion
    }
}

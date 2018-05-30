using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.BindingModel.ShipmentInfoBindingModel
{
    public class ShipmentInfoBindingModel
    {
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
        //public string CustomerId { get; set; }
        #endregion

        #region District
        public long DistrictId { get; set; }
        #endregion

        #region Create ShipmentInfo Model
        public ShipmentInfo Create(ApplicationUser user)
        {
            return new ShipmentInfo()
            {
                Phone = this.Phone,
                ReceivedPersonName = this.ReceivedPersonName,
                Address = this.Address,
                CustomerId = user.Id,
                DistrictId = this.DistrictId,
            };
        }
        #endregion
    }
}

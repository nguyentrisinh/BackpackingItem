using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class ShipmentInfoReturnModel
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
        //public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        #endregion

        #region District
        //public District District { get; set; }
        public long DistrictId { get; set; }
        #endregion

        public static ShipmentInfoReturnModel Create(ShipmentInfo shipmentInfo)
        {
            return new ShipmentInfoReturnModel()
            {
                Id = shipmentInfo.Id,
                Phone = shipmentInfo.Phone,
                ReceivedPersonName = shipmentInfo.ReceivedPersonName,
                Address = shipmentInfo.Address,
                CustomerId = shipmentInfo.CustomerId,
                DistrictId = shipmentInfo.DistrictId,
            };
        }

        public static List<ShipmentInfoReturnModel> Create(List<ShipmentInfo> shipmentInfos)
        {
            List<ShipmentInfoReturnModel> shipmentInfosReturnModel = new List<ShipmentInfoReturnModel>();

            foreach (var shipmentInfo in shipmentInfos)
            {
                ShipmentInfoReturnModel shipmentInfoReturnModel = ShipmentInfoReturnModel.Create(shipmentInfo);
                shipmentInfosReturnModel.Add(shipmentInfoReturnModel);
            }

            return shipmentInfosReturnModel;
        }
    }
}

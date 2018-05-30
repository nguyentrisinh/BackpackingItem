using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class DistrictReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region ShippingFee
        public decimal ShippingFee { get; set; }
        #endregion

        #region City
        public CityReturnModel City { get; set; }
        public long CityId { get; set; }
        #endregion

        #region Orders
        //public List<Order> Orders { get; set; }
        #endregion

        #region ShipmentInfos
        //public List<ShipmentInfo> ShipmentInfos { get; set; }
        #endregion

        public static DistrictReturnModel Create(District district)
        {
            return new DistrictReturnModel()
            {
                Id = district.Id,
                Name = district.Name,
                ShippingFee = district.ShippingFee,
                City = null,
                CityId = district.CityId,
            };
        }

        public static DistrictReturnModel CreateWithCity(District district)
        {
            return new DistrictReturnModel()
            {
                Id = district.Id,
                Name = district.Name,
                ShippingFee = district.ShippingFee,
                City = district.City == null ? null : CityReturnModel.CreateNoDistrict(district.City),
                CityId = district.CityId,
            };
        }
    }
}

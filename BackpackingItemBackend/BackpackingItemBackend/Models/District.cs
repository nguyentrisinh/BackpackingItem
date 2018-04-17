﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class District
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
        public City City { get; set; }
        public long CityId { get; set; }
        #endregion

        #region Orders
        public List<Order> Orders { get; set; }
        #endregion

        #region ShipmentInfos
        public List<ShipmentInfo> ShipmentInfos { get; set; }
        #endregion
    }
}

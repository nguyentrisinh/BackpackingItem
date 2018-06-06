﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.BindingModel.OrderBindingModel
{
    public class OrderBindingModel
    {
        #region DateTime
        public DateTime Datetime { get; set; }
        #endregion

        #region TotalPrice
        public decimal TotalPrice { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        #endregion

        #region ReceivePersonName 
        public string ReceivePersonName { get; set; }
        #endregion

        #region Phone
        public string Phone { get; set; }
        #endregion

        #region Status
        public OrderStatus Status { get; set; } = OrderStatus.InProccess;
        #endregion

        #region VoucherId
        public long? VoucherId { get; set; }
        #endregion

        #region DistrictId
        public long DistrictId { get; set; }
        #endregion

        #region List OrderDetailBindingModel
        public List<OrderDetailBindingModel> OrderDetails { get; set; }
        #endregion

        public Order Create(ApplicationUser user)
        {
            return new Order()
            {
                Datetime = this.Datetime,
                TotalPrice = this.TotalPrice,
                Address = this.Address,
                ReceivePersonName = this.ReceivePersonName,
                Phone = this.Phone,
                Status = OrderStatus.InProccess,
                VoucherId = this.VoucherId,
                DistrictId = this.DistrictId,
                CustomerId = user.Id,
            };
        }
    }
}

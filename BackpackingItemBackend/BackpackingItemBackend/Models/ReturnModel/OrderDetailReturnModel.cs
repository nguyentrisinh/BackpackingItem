using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class OrderDetailReturnModel
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Quantity
        public int Quantity { get; set; }
        #endregion

        #region PricePerUnit
        public decimal PricePerUnit { get; set; }
        #endregion

        #region TotalPrice
        public decimal TotalPrice { get; set; }
        #endregion

        #region Variant 
        public VariantReturnModel Variant { get; set; }
        public long VariantId { get; set; }
        #endregion

        #region Order
        public long OrderId { get; set; }
        #endregion

        public static OrderDetailReturnModel Create(OrderDetail orderDetail)
        {
            return new OrderDetailReturnModel()
            {
                Id = orderDetail.Id,
                Quantity = orderDetail.Quantity,
                PricePerUnit = orderDetail.PricePerUnit,
                TotalPrice = orderDetail.TotalPrice,
                Variant = orderDetail.Variant == null ? null : VariantReturnModel.Create(orderDetail.Variant),
                VariantId = orderDetail.VariantId,
                OrderId = orderDetail.OrderId,
            };
        }

    }
}

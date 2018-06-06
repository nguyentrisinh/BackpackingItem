using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.BindingModel.OrderBindingModel
{
    public class OrderDetailBindingModel
    {
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
        public long VariantId { get; set; }
        #endregion

        public OrderDetail Create(long orderId)
        {
            return new OrderDetail()
            {
                Quantity = this.Quantity,
                PricePerUnit = this.PricePerUnit,
                TotalPrice = this.TotalPrice,
                VariantId = this.VariantId,
                OrderId = orderId,
            };
        }
    }
}

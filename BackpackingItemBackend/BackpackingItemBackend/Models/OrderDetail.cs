using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class OrderDetail
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
        public Variant Variant { get; set; }
        public long VariantId { get; set; }
        #endregion

        #region Order
        public Order Order { get; set; }
        public long OrderId { get; set; }
        #endregion
    }
}

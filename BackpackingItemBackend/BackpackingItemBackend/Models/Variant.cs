using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Variant
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region OldPrice 
        public decimal OldPrice { get; set; }
        #endregion

        #region OfficialPrice 
        public decimal OfficialPrice { get; set; }
        #endregion

        #region VariantStatus
        public VariantStatus VariantStatus { get; set; }
        #endregion

        #region Product
        public virtual Product Product { get; set; }
        public long ProductId { get; set; }
        #endregion

        #region Size 
        public virtual Size Size { get; set; }
        public long SizeId { get; set; }
        #endregion

        #region Color
        public virtual Color Color { get; set; }
        public long ColorId { get; set; }
        #endregion

        #region Images
        public List<Image> Images { get; set; }
        #endregion

        #region OrderDetails
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion

    }

    public enum VariantStatus
    {
        Instock = 1,
        OutOfStock = 2
    }
}


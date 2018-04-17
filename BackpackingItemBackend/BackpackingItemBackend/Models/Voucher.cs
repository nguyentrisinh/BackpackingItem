using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Voucher
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Method
        public string Method { get; set; }
        #endregion

        #region Code 
        public string Code { get; set; }
        #endregion

        #region Value 
        public float Value { get; set; }
        #endregion

        #region Quantity
        public int Quantity { get; set; }
        #endregion

        #region RemainQuantity
        public int RemainQuantity { get; set; }
        #endregion

        #region Orders
        public List<Order> Orders { get; set;  }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Order
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region TransactionNumber
        public Guid TransactionNumber { get; set; }
        #endregion

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
        public OrderStatus Status { get; set; }
        #endregion

        #region Voucher
        public virtual Voucher Voucher { get; set; }
        public long ? VoucherId { get; set; }
        #endregion

        #region Customer
        public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        #endregion

        #region District
        public District District { get; set; }
        public long DistrictId { get; set; }
        #endregion

        #region OrderDetails
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region Contructor
        public Order()
        {
            this.TransactionNumber = Guid.NewGuid();
        }
        #endregion

    }
    public enum OrderStatus
    {
        InProccess = 1,
        Approved = 2,
        InTransit = 3,
        Paid = 4,
        Received = 5
    }
}

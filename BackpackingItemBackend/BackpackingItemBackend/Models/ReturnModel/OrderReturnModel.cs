using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class OrderReturnModel
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
        //public virtual Voucher Voucher { get; set; }
        public long? VoucherId { get; set; }
        #endregion

        #region Customer
        //public ApplicationUser Customer { get; set; }
        public string CustomerId { get; set; }
        #endregion

        #region District
        //public District District { get; set; }
        public long DistrictId { get; set; }
        #endregion

        #region OrderDetails
        public List<OrderDetailReturnModel> OrderDetails { get; set; }
        #endregion

        public static OrderReturnModel Create(Order order)
        {
            List<OrderDetailReturnModel> orderDetailReturnModels = new List<OrderDetailReturnModel>();

            if (order.OrderDetails != null)
            {
                foreach (var orderDetail in order.OrderDetails)
                {
                    OrderDetailReturnModel orderDetailReturnItem = OrderDetailReturnModel.Create(orderDetail);
                    orderDetailReturnModels.Add(orderDetailReturnItem);
                }
            }

            return new OrderReturnModel()
            {
                Id = order.Id,
                TransactionNumber = order.TransactionNumber,
                Datetime = order.Datetime,
                TotalPrice = order.TotalPrice,
                Address = order.Address,
                ReceivePersonName = order.ReceivePersonName,
                Phone = order.Phone,
                Status = order.Status,
                VoucherId = order.VoucherId,
                CustomerId = order.CustomerId,
                DistrictId = order.DistrictId,
                OrderDetails = order.OrderDetails == null ? null : orderDetailReturnModels,
            };
        }

        public static List<OrderReturnModel> Create(List<Order> orders)
        {
            List<OrderReturnModel> ordersReturnModel = new List<OrderReturnModel>();

            foreach (var order in orders)
            {
                OrderReturnModel orderReturnModel = OrderReturnModel.Create(order);
                ordersReturnModel.Add(orderReturnModel);
            }

            return ordersReturnModel;
        }
    }
}

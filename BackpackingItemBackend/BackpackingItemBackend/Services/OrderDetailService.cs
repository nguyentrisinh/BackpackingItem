using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.BindingModel.OrderBindingModel;
using Lib.Web.Services;

namespace BackpackingItemBackend.Services
{
    public interface IOrderDetailService
    {
        OrderDetail CreateOrderDetail(long orderId, OrderDetailBindingModel orderBindindModel);

        Task<List<OrderDetail>> CreateListOrderDetail(Order order, List<OrderDetailBindingModel> orderDetailBindingModel);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public OrderDetailService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        #region CreateListOrderDetail
        
        #region CreateOrderDetail 
        public OrderDetail CreateOrderDetail(long orderId, OrderDetailBindingModel orderBindindModel)
        {
            OrderDetail orderDetail = orderBindindModel.Create(orderId);
            _context.OrderDetails.Add(orderDetail);

            _context.SaveChangesAsync();
            return orderDetail;
        }
        #endregion

        #region CreateListOrderDetail
        public async Task<List<OrderDetail>> CreateListOrderDetail(Order order, List<OrderDetailBindingModel> orderDetailBindingModelList)
        {
            // Add a single OrderDetail in orderDetailList into DbContext
            foreach (OrderDetailBindingModel orderDetailBindingModel in orderDetailBindingModelList)
            {
                OrderDetail orderDetail = orderDetailBindingModel.Create(order.Id);

                _context.OrderDetails.Add(orderDetail);
            }

            // Save Changes
            await _context.SaveChangesAsync();

            // Get OrderDetailList
            List<OrderDetail> orderDetailList = _context.OrderDetails
                .Where(ent => ent.OrderId == order.Id)
                .ToList();
                

            return orderDetailList;
        }
        #endregion

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.ReturnModel;
using BackpackingItemBackend.PagingParam;
using Lib.Web.Models;
using Lib.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace BackpackingItemBackend.Services
{
    public interface IOrderService
    {
        Order GetById(long id);

        Order GetByTransactionNumber(Guid TransactionNumber);

        PagedList<OrderReturnModel> GetByUserId(OrderPagingParams pagingParams, string userId);

        Task<Order> SaveOrder(Order order);
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public OrderService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        #region GetById
        public Order GetById(long id)
        {
            try
            {
                var order = _context.Orders
                    .Include(ent => ent.OrderDetails)
                    .Include(ent => ent.Voucher)
                    .Include(ent => ent.Customer)
                    .Include(ent => ent.District)
                    .First(ent => ent.Id == id);

                return order;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2400), HttpStatusCode.BadRequest);
                return new Order();
            }
        }
        #endregion

        #region GetByTransactionNumber
        public Order GetByTransactionNumber(Guid TransactionNumber)
        {
            try
            {
                var order = _context.Orders
                    .Include(ent => ent.OrderDetails)
                    .Include(ent => ent.Voucher)
                    .Include(ent => ent.Customer)
                    .Include(ent => ent.District)
                    .First(ent => ent.TransactionNumber == TransactionNumber);

                return order;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2400), HttpStatusCode.BadRequest);
                return new Order();
            }
        }
        #endregion

        #region GetListByUserId
        public PagedList<OrderReturnModel> GetByUserId(OrderPagingParams pagingParams,string userId)
        {
            try
            {
                var query = _context.Orders
                    .Include(ent => ent.OrderDetails)
                    .Include(ent => ent.Voucher)
                    .Include(ent => ent.Customer)
                    .Include(ent => ent.District)
                    .Where(ent => ent.CustomerId == userId)
                    .AsQueryable();

                #region GET paging for ProductReturnModel
                List<Order> orders = query.Skip(pagingParams.PageSize * (pagingParams.PageNumber - 1))
                    .Take(pagingParams.PageSize)
                    .ToList();
                #endregion

                return new PagedList<OrderReturnModel>(OrderReturnModel.Create(orders), pagingParams.PageNumber, pagingParams.PageSize, query.Count());
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2300), HttpStatusCode.BadRequest);
                return new PagedList<OrderReturnModel>();
            }
        }
        #endregion

        #region SaveOrder
        public async Task<Order> SaveOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }
        #endregion
    }
}

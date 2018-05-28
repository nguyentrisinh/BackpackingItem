using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.PagingParam;
using Lib.Web.Models;
using Lib.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace BackpackingItemBackend.Services
{
    public interface IOrderService
    {
        Order GetById(long id);

        PagedList<Order> GetByUserId(OrderPagingParams pagingParams, string userId);
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
                throwService.ThrowApiException(ErrorsDefine.Find(2300), HttpStatusCode.BadRequest);
                return new Order();
            }
        }
        #endregion

        #region GetListByUserId
        public PagedList<Order> GetByUserId(OrderPagingParams pagingParams,string userId)
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

                return new PagedList<Order>(query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2300), HttpStatusCode.BadRequest);
                return new PagedList<Order>();
            }
        }
        #endregion
    }
}

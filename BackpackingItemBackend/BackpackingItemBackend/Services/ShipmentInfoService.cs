using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.ReturnModel;
using BackpackingItemBackend.PagingParam;
using Lib.Web.Models;
using Lib.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Services
{
    public interface IShipmentInfoService
    {
        Task<ShipmentInfo> SaveShipmentInfo(ShipmentInfo model);

        PagedList<ShipmentInfoReturnModel> GetByUserId(ShipmentInfoPagingParams pagingParams, string userId);
    }

    public class ShipmentInfoService : IShipmentInfoService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public ShipmentInfoService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        #region SaveShipmentInfo
        public async Task<ShipmentInfo> SaveShipmentInfo (ShipmentInfo model)
        {
            _context.ShipmentInfos.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }
        #endregion

        #region GetListByUserId
        public PagedList<ShipmentInfoReturnModel> GetByUserId(ShipmentInfoPagingParams pagingParams, string userId)
        {
            try
            {
                var query = _context.ShipmentInfos
                    .Where(ent => ent.CustomerId == userId)
                    .AsQueryable();

                #region GET paging for ProductReturnModel
                List<ShipmentInfo> shipmentInfos = query.Skip(pagingParams.PageSize * (pagingParams.PageNumber - 1))
                    .Take(pagingParams.PageSize)
                    .ToList();
                #endregion

                return new PagedList<ShipmentInfoReturnModel>(ShipmentInfoReturnModel.Create(shipmentInfos), pagingParams.PageNumber, pagingParams.PageSize, query.Count());
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2200), HttpStatusCode.BadRequest);
                return new PagedList<ShipmentInfoReturnModel>();
            }
        }
        #endregion

    }
}

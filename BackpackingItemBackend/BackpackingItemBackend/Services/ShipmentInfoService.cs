using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.BindingModel.ShipmentInfoBindingModel;
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

        ShipmentInfo GetById(long shipmentInfoId);

        ShipmentInfo GetByIdCurrent(long shipmentInfoId, ApplicationUser applicationUser);

        void DeleteByIdCurrent(long shipmentInfoId, ApplicationUser applicationUser);

        ShipmentInfo UpdateByIdCurrent(long shipmentInfoId, ShipmentInfoBindingModel model, ApplicationUser applicationUser);
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

        #region GetById
        public ShipmentInfo GetById(long shipmentInfoId)
        {
            try
            {
                var product = _context.ShipmentInfos
                    .First(ent => ent.Id == shipmentInfoId);

                return product;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2700), HttpStatusCode.BadRequest);
                return new ShipmentInfo();
            }
        }

        public ShipmentInfo GetByIdCurrent(long shipmentInfoId, ApplicationUser applicationUser)
        {
            try
            {
                var shipmentInfo = _context.ShipmentInfos
                    .First(ent => ent.Id == shipmentInfoId);

                if (applicationUser.Id != shipmentInfo.CustomerId)
                {
                    throwService.ThrowApiException(ErrorsDefine.Find(2700), HttpStatusCode.BadRequest);
                }

                return shipmentInfo;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2700), HttpStatusCode.BadRequest);
                return new ShipmentInfo();
            }
        }
        #endregion

        #region DeleteById
        public void DeleteByIdCurrent(long shipmentInfoId, ApplicationUser applicationUser)
        {
            ShipmentInfo shipmentInfo = this.GetByIdCurrent(shipmentInfoId, applicationUser);
            _context.ShipmentInfos.Remove(shipmentInfo);
            _context.SaveChanges();

        }
        #endregion

        #region UpdateShipmentInfo
        public ShipmentInfo UpdateByIdCurrent(long shipmentInfoId, ShipmentInfoBindingModel model, ApplicationUser applicationUser)
        {
            ShipmentInfo shipmentInfo = this.GetByIdCurrent(shipmentInfoId, applicationUser);

            shipmentInfo.Phone = model.Phone;
            shipmentInfo.ReceivedPersonName = model.ReceivedPersonName;
            shipmentInfo.Address = model.Address;
            shipmentInfo.DistrictId = model.DistrictId;

            _context.SaveChanges();

            return shipmentInfo;
        }
        #endregion

    }
}

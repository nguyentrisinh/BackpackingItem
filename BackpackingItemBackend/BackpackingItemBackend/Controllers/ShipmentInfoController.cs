using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.BindingModel.ShipmentInfoBindingModel;
using BackpackingItemBackend.Models.ReturnModel;
using BackpackingItemBackend.PagingParam;
using BackpackingItemBackend.Services;
using Lib.Web.Controllers;
using Lib.Web.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ShipmentInfoController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected IShipmentInfoService shipmentInfoService;

        protected IAccountService accountService;

        #region Contructor

        public ShipmentInfoController(ApplicationDbContext context, IShipmentInfoService services, IThrowService throwService, IAccountService accountService) : base(throwService)
        {
            _context = context;
            shipmentInfoService = services;
            this.accountService = accountService;
        }
        #endregion

        #region SaveShipmentInfo
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("")]
        public async Task<IActionResult> SaveShipmentInfo([FromBody] ShipmentInfoBindingModel model)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                #region SaveShipmentInfo
                ShipmentInfo shipmentInfo = await this.shipmentInfoService.SaveShipmentInfo(model.Create(user));
                #endregion

                return await this.AsSuccessResponse(ShipmentInfoReturnModel.Create(shipmentInfo));

            }

            return await this.AsSuccessResponse("Test", HttpStatusCode.OK);
        }
        #endregion

        #region GetShipmentInfosByCurrent
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[action]")]
        public async Task<IActionResult> GetShipmentInfosCurrent(ShipmentInfoPagingParams shipmentInfoPagingParams)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                #region GetShipmentInfoByUserId
                var pagedListShipmentInfoReturnModel = this.shipmentInfoService.GetByUserId(shipmentInfoPagingParams, user.Id);
                #endregion

                return await this.AsSuccessResponse(pagedListShipmentInfoReturnModel, HttpStatusCode.OK);

            }

            return await this.AsSuccessResponse("Test", HttpStatusCode.OK);
        }
        #endregion

        #region GetShipmentInfoById
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[action]/{id:long}")]
        public async Task<IActionResult> GetShipmentInfoByIdCurrent(long id)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                ShipmentInfo shipmentInfo = this.shipmentInfoService.GetByIdCurrent(id, user);
                ShipmentInfoReturnModel shipmentInfoReturnModel = ShipmentInfoReturnModel.Create(shipmentInfo);
                return await this.AsSuccessResponse(shipmentInfoReturnModel, HttpStatusCode.OK);
            }

            return await this.AsSuccessResponse("Test", HttpStatusCode.OK);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("{shipmentInfoId:long}")]
        public async Task<IActionResult> GetById(long shipmentInfoId)
        {
            ShipmentInfo shipmentInfo = this.shipmentInfoService.GetById(shipmentInfoId);
            ShipmentInfoReturnModel shipmentInfoReturnModel = ShipmentInfoReturnModel.Create(shipmentInfo);
            return await this.AsSuccessResponse(shipmentInfoReturnModel, HttpStatusCode.OK);
        }
        #endregion

        #region DeleteById
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[action]/{id:long}")]
        public async Task<IActionResult> DeleteShipmentInfoByIdCurrent(long id)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                this.shipmentInfoService.DeleteByIdCurrent(id, user);
                return await this.AsSuccessResponse("Delete successfully", HttpStatusCode.OK);
            }

            return await this.AsSuccessResponse("Test", HttpStatusCode.OK);
        }
        #endregion

        #region UpdateById
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[action]/{id:long}")]
        public async Task<IActionResult> UpdateByIdCurrent(long id, [FromBody] ShipmentInfoBindingModel model)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                ShipmentInfo shipmentInfo = this.shipmentInfoService.UpdateByIdCurrent(id, model, user);
                return await this.AsSuccessResponse(ShipmentInfoReturnModel.Create(shipmentInfo), HttpStatusCode.OK);
            }

            return await this.AsSuccessResponse("Test", HttpStatusCode.OK);

        }
        #endregion
    }
}
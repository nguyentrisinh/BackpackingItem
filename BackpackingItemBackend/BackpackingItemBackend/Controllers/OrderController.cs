using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.BindingModel.OrderBindingModel;
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
    public class OrderController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected IOrderService orderService;

        protected IAccountService accountService;

        protected IOrderDetailService orderDetailService;

        #region Contructor

        public OrderController(
            ApplicationDbContext context, 
            IOrderService services,
            IThrowService throwService, 
            IAccountService accountService,
            IOrderDetailService orderDetailService
        ) : base(throwService)
        {
            _context = context;
            orderService = services;
            this.accountService = accountService;
            this.orderDetailService = orderDetailService;
        }

        #endregion

        #region GetById
        [HttpGet]
        [Route("{orderId:long}")]
        public async Task<IActionResult> GetById(long orderId)
        {
            Order order = this.orderService.GetById(orderId);
            OrderReturnModel productReturnModel = OrderReturnModel.Create(order);
            return await this.AsSuccessResponse(productReturnModel, HttpStatusCode.OK);
        }
        #endregion

        #region GetByTransactionNumber
        [HttpGet]
        [Route("[action]/{TransactionNumber:Guid}")]
        public async Task<IActionResult> GetByTransactionNumber(Guid TransactionNumber)
        {
            Order order = this.orderService.GetByTransactionNumber(TransactionNumber);
            OrderReturnModel productReturnModel = OrderReturnModel.Create(order);
            return await this.AsSuccessResponse(productReturnModel, HttpStatusCode.OK);
        }
        #endregion

        #region GetOrdersByCurrent
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[action]")]
        public async Task<IActionResult> GetOrderCurrent(OrderPagingParams orderPagingParams)
        {
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                #region GetOrderByUserId
                var pagedListOrderReturnModel = this.orderService.GetByUserId(orderPagingParams, user.Id);
                #endregion

                return await this.AsSuccessResponse(pagedListOrderReturnModel, HttpStatusCode.OK);


            }

            return await this.AsSuccessResponse("Test", HttpStatusCode.OK);
        }
        #endregion

        #region PostOrder
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("[action]")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderBindingModel model)
        {

            var currentUser = HttpContext.User;

            //if (currentUser.HasClaim(c => c.Type == ClaimTypes.Email))
            if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.Jti))
            {
                #region getUser
                var username = currentUser.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
                ApplicationUser user = this.accountService.GetById(new Guid(username));
                #endregion

                #region create Order
                Order order = model.Create(user);
                #endregion

                #region SaveOrder
                Order savedOrder = await this.orderService.SaveOrder(order);
                #endregion

                #region Save OrderDetailBindingModel List
                List<OrderDetailBindingModel> orderDetailBindingModelList = model.OrderDetails;

                List<OrderDetail> orderDetails = await this.orderDetailService.CreateListOrderDetail(order, orderDetailBindingModelList);
                #endregion

                #region GetReturnOrder 
                Order orderReturn = this.orderService.GetById(order.Id);
                #endregion

                return await this.AsSuccessResponse(OrderReturnModel.Create(orderReturn), HttpStatusCode.OK);
            }

            return await this.AsSuccessResponse("test", HttpStatusCode.OK);
        }
        #endregion
    }
}
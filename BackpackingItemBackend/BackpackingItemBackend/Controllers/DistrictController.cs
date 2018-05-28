using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Services;
using Lib.Web.Controllers;
using Lib.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class DistrictController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected IOrderService orderService;

        protected IAccountService accountService;

        #region Contructor

        public DistrictController(ApplicationDbContext context, IOrderService services, IThrowService throwService, IAccountService accountService) : base(throwService)
        {
            _context = context;
            orderService = services;
            this.accountService = accountService;
        }
        #endregion
    }
}
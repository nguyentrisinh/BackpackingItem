using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lib.Web.Controllers;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Services;
using Lib.Web.Services;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.ReturnModel;
using System.Net;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class VariantController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected IVariantService variantService;

        #region Contructor

        public VariantController(ApplicationDbContext context, IVariantService services, IThrowService throwService) : base(throwService)
        {
            _context = context;
            variantService = services;
        }

        #endregion

        #region GetById
        [HttpGet]
        [Route("{variantId:long}")]
        public async Task<IActionResult> GetById(long variantId)
        {
            Variant variant = this.variantService.GetById(variantId);
            VariantReturnModel variantReturnModel = VariantReturnModel.Create(variant);
            return await this.AsSuccessResponse(variantReturnModel, HttpStatusCode.OK);
        }
        #endregion
    }
}
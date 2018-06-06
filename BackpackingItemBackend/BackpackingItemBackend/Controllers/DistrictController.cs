using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.ReturnModel;
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

        protected IDistrictService districtService;

        #region Contructor

        public DistrictController(ApplicationDbContext context, IDistrictService services, IThrowService throwService) : base(throwService)
        {
            _context = context;
            districtService = services;
        }
        #endregion

        #region GetById
        [HttpGet]
        [Route("{districtId:long}")]
        public async Task<IActionResult> GetById(long districtId)
        {
            District district = this.districtService.GetById(districtId);

            return await this.AsSuccessResponse(DistrictReturnModel.CreateWithCity(district), HttpStatusCode.OK);
        }
        #endregion

    }
}
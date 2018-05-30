using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
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
    public class CityController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected ICityService cityService;


        #region Contructor

        public CityController(ApplicationDbContext context, ICityService services, IThrowService throwService) : base(throwService)
        {
            _context = context;
            cityService = services;
        }
        #endregion

        #region GetAll
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var cities = this.cityService.GetAll();

            return await this.AsSuccessResponse(CityReturnModel.Create(cities), HttpStatusCode.OK);
        }
        #endregion

        #region GetById
        [HttpGet]
        [Route("{cityId:long}")]
        public async Task<IActionResult> GetById(long cityId)
        {
            var city = this.cityService.GetById(cityId);

            return await this.AsSuccessResponse(CityReturnModel.Create(city), HttpStatusCode.OK);
        }
        #endregion
    }
}
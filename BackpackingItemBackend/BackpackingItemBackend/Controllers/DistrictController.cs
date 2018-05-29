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

        protected IDistrictService districtService;


        #region Contructor

        public DistrictController(ApplicationDbContext context, IDistrictService services, IThrowService throwService) : base(throwService)
        {
            _context = context;
            districtService = services;
        }
        #endregion
    }
}
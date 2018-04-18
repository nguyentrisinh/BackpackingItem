using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models.ReturnModel;
using Lib.Web.Controllers;
using Lib.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BackpackingItemBackend.Constants;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SubCategoriesController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;


        #region Contructor

        public SubCategoriesController(ApplicationDbContext context, IThrowService throwService) : base(throwService)
        {
            _context = context;
        }

        #endregion

        [HttpGet]
        [Route("{subCategoryId:long}")]
        public async Task<IActionResult> GetById(long subCategoryId)
        {
            try
            {
                var subCategory = _context.SubCategories
                    .Include(ent => ent.Products)
                    .First(ent => ent.Id == subCategoryId);

                return await this.AsSuccessResponse(SubCategoryReturnModel.Create(subCategory), HttpStatusCode.OK);
            }
            catch (InvalidOperationException)
            {
                ThrowService.ThrowApiException(ErrorsDefine.Find(2000), HttpStatusCode.BadRequest);
                return await this.AsSuccessResponse(null, HttpStatusCode.OK);
            }
        }
    }
}
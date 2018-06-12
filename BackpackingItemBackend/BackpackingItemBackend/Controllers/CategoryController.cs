using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lib.Web.Controllers;
using Lib.Web.Services;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models.ReturnModel;
using Microsoft.EntityFrameworkCore;
using System.Net;
using BackpackingItemBackend.Constants;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CategoryController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;


        #region Contructor

        public CategoryController(ApplicationDbContext context, IThrowService throwService) : base(throwService)
        {
            _context = context;
        }

        #endregion

        #region Method

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCategoryList()
        {
            var category = _context.Categories
                .Include(ent => ent.SubCategories)
                    .ThenInclude(ent => ent.Products)
                .ToList();

            List<CategoryReturnModel> categories = new List<CategoryReturnModel>();

            foreach (var categoryObject in category)
            {
                CategoryReturnModel categoryReturnItem = CategoryReturnModel.Create(categoryObject);
                categories.Add(categoryReturnItem);
            }


            return await this.AsSuccessResponse(categories, HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("{categoryId:long}")]
        public async Task<IActionResult> GetById(long categoryId)
        {
            try
            {
                var category = _context.Categories
                    .Include(ent => ent.SubCategories)
                        .ThenInclude(ent => ent.Products)
                    .First(ent => ent.Id == categoryId);

                return await this.AsSuccessResponse(CategoryReturnModel.Create(category), HttpStatusCode.OK);
            }
            catch (InvalidOperationException)
            {
                ThrowService.ThrowApiException(ErrorsDefine.Find(1900), HttpStatusCode.BadRequest);
                return await this.AsSuccessResponse(null, HttpStatusCode.OK);
            }
        }

        #endregion
    }
}
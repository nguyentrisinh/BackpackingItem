using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Lib.Web.Controllers;
using Lib.Web.Services;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models.ReturnModel;
using Microsoft.EntityFrameworkCore;
using System.Net;

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

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetCategoryList()
        {
            var category = _context.Categories
                .Include(ent => ent.SubCategories)
                    //.ThenInclude(ent => ent.Products)
                .ToList();

            List<CategoryReturnModel> categories = new List<CategoryReturnModel>();

            foreach (var categoryObject in category)
            {
                CategoryReturnModel categoryReturnItem = CategoryReturnModel.Create(categoryObject);
                categories.Add(categoryReturnItem);
            }


            return await this.AsSuccessResponse(categories, HttpStatusCode.OK);
        }
    }
}
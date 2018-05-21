using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.BindingModel.LoginBindingModel;
using BackpackingItemBackend.Models.BindingModel.RegisterBindingModel;
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
    public class AccountController : ApiControllerBase
    {
        private readonly ApplicationDbContext _context;

        protected IAccountService accountService;

        #region Contructor

        public AccountController(ApplicationDbContext context, IAccountService services, IThrowService throwService) : base(throwService)
        {
            _context = context;
            accountService = services;
        }

        #endregion

        #region CustomerRegister
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CustomerRegister([FromBody] RegisterBindingModel model)
        {
            var applicationUser = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                Birthday = model.Birthday,
                Status = Status.Active,
                Role = Role.Customer,
            };

            await this.accountService.CreateUser(applicationUser, model.Password);

            return await this.AsSuccessResponse(ApplicationUserReturnModel.Create(applicationUser), HttpStatusCode.OK);

        }
        #endregion

        #region GetById
        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetById(Guid userId)
        {
            ApplicationUser applicationUser = this.accountService.GetById(userId);
            return await this.AsSuccessResponse(ApplicationUserReturnModel.Create(applicationUser), HttpStatusCode.OK);
        }
        #endregion

        #region Login
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] LoginBindingModel model)
        {
            string token = await this.accountService.Login(model);
            return await this.AsSuccessResponse(token, HttpStatusCode.OK);
        }
        #endregion

    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BackpackingItemBackend.BaseControllers;
using BackpackingItemBackend.BaseServices;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    //public class StartupController : Controller
    public class StartupController : ApiControllerBase
    {

        #region Contructor

        public StartupController(IThrowService throwService) : base(throwService)
        {

        }

        #endregion

        //#region Contructor

        //public StartupController()
        //{

        //}

        //#endregion

        [HttpGet, Authorize]
        public async Task<IActionResult> Get()
        {
            var currentUser = HttpContext.User;
            int userAge = 0;

            var resultBookList = new Book[] {
              new Book { Author = "Ray Bradbury", Title = "Fahrenheit 451", AgeRestriction = false },
              new Book { Author = "Gabriel García Márquez", Title = "One Hundred years of Solitude", AgeRestriction = false },
              new Book { Author = "George Orwell", Title = "1984", AgeRestriction = false },
              new Book { Author = "Anais Nin", Title = "Delta of Venus", AgeRestriction = true }
            };


            if (currentUser.HasClaim(c => c.Type == ClaimTypes.DateOfBirth))
            {
                DateTime birthDate = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth).Value);
                userAge = DateTime.Today.Year - birthDate.Year;
            }

            if (userAge < 18)
            {
                resultBookList = resultBookList.Where(b => !b.AgeRestriction).ToArray();
            }
            //return resultBookList;
            return await this.AsSuccessResponse(resultBookList, HttpStatusCode.OK);

        }

        [HttpGet, Authorize]
        [Route("[action]")]
        public async Task<IActionResult> GetException()
        {
            var currentUser = HttpContext.User;

            ThrowService.ThrowApiException(9999, "This is the Error Message", HttpStatusCode.BadRequest);

            return await this.AsSuccessResponse(currentUser, HttpStatusCode.OK);
        }

        public class Book
        {
            public string Author { get; set; }
            public string Title { get; set; }
            public bool AgeRestriction { get; set; }
        }
    }
}
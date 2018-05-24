using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.DataContext;
using Lib.Web.Services;
using BackpackingItemBackend.Models;
using BackpackingItemBackend.Models.BindingModel.RegisterBindingModel;
using Microsoft.AspNetCore.Identity;
using BackpackingItemBackend.Constants;
using System.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using BackpackingItemBackend.Models.BindingModel.LoginBindingModel;

namespace BackpackingItemBackend.Services
{
    public interface IAccountService
    {
        Task CreateUser(ApplicationUser applicationUser, string password);

        ApplicationUser GetById(Guid applicationUserId);

        Task<string> Login(LoginBindingModel model);

        ApplicationUser GetCurrent(string username);

        ApplicationUser UpdateUser(RegisterBindingModel model, string applicationUserId);
    }


    public class AccountService : IAccountService
    {
        UserManager<ApplicationUser> UserManager;

        private readonly SignInManager<ApplicationUser> _signInManager;

        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        private IConfiguration _config;

        #region Constructor
        public AccountService(
            ApplicationDbContext context, 
            IThrowService throwService, 
            UserManager<ApplicationUser> userManager, 
            IConfiguration config,
            SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            this.throwService = throwService;
            UserManager = userManager;
            _config = config;
            _signInManager = signInManager;
        }
        #endregion

        #region CreateUser
        public async Task CreateUser(ApplicationUser applicationUser, string password)
        {
            var result = await this.UserManager.CreateAsync(applicationUser, password);

            if (!result.Succeeded)
            {
                throwService.ThrowApiException(result);
            }

        }
        #endregion

        #region GetUserById

        public ApplicationUser GetById(Guid applicationUserId)
        {
            try
            {
                var applicationUser = _context.ApplicationUsers
                    .First(ent => ent.Id == applicationUserId.ToString());

                return applicationUser;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2200), HttpStatusCode.BadRequest);
                return new ApplicationUser();
            }
        }

        #endregion

        #region Login
        public async Task<string> Login(LoginBindingModel model)
        {
            var user = await this.Authenticate(model);
            
            if (user != null)
            {
                var tokenString = BuildToken(user);
                return tokenString;
            }

            return null;
        }

        private string BuildToken(ApplicationUser user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthday.ToString("yyyy-MM-dd")),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(Constant.TokenLastingTime),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<ApplicationUser> Authenticate(LoginBindingModel login)
        {
            try
            {
                ApplicationUser user = _context.ApplicationUsers.First(ent => ent.UserName == login.Username);

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                //var result = await _signInManager.PasswordSignInAsync(user, login.Password, model.RememberMe, lockoutOnFailure: false);
                var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    return user;
                }
                else
                {
                    this.throwService.ThrowApiException(ErrorsDefine.Find(2201), HttpStatusCode.Unauthorized);
                    return null;
                }
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2201), HttpStatusCode.Unauthorized);
                return null;
            }
        }

        #endregion

        #region GetCurrent 
        public ApplicationUser GetCurrent(string username)
        {
            try
            {
                ApplicationUser user = _context.ApplicationUsers.First(ent => ent.UserName == username);

                return user;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2200), HttpStatusCode.BadRequest);
                return new ApplicationUser();
            }
        }
        #endregion

        #region UpdateUser
        public ApplicationUser UpdateUser(RegisterBindingModel model, string applicationUserId)
        {
            try
            {
                var applicationUser = _context.ApplicationUsers
                    .First(ent => ent.Id == applicationUserId.ToString());

                #region update User Profile
                applicationUser.Birthday = model.Birthday;
                applicationUser.FirstName = model.FirstName;
                applicationUser.LastName = model.LastName;
                applicationUser.Gender = model.Gender;

                _context.ApplicationUsers.Update(applicationUser);
                _context.SaveChanges();
                #endregion

                return applicationUser;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2200), HttpStatusCode.BadRequest);
                return new ApplicationUser();
            }
        }
        #endregion
    }
}

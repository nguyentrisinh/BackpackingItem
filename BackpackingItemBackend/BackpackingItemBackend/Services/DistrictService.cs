using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using Lib.Web.Models;
using Lib.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Services
{
    public interface IDistrictService
    {
        District GetById(long id);

    }
    public class DistrictService : IDistrictService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public DistrictService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        #region GetById
        public District GetById(long id)
        {
            try
            {
                var district = _context.Districts
                    .Include(ent => ent.City)
                    .First(ent => ent.Id == id);

                return district;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2600), HttpStatusCode.BadRequest);
                return new District();
            }
        }
        #endregion

    }
}

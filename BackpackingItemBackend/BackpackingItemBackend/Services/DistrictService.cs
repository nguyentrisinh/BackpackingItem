using BackpackingItemBackend.DataContext;
using Lib.Web.Models;
using Lib.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Services
{
    public interface IDistrictService
    {
        
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


    }
}

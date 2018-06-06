using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using Lib.Web.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Services
{
    public interface ICityService
    {
        List<City> GetAll();

        City GetById(long cityId);
    }

    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public CityService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        #region GetAll
        public List<City> GetAll()
        {
            var cities = _context.Cities
                .Include(ent => ent.Districts)
                .ToList();

            return cities;
        }
        #endregion

        #region GetById
        public City GetById(long cityId)
        {
            try
            {
                var city = _context.Cities
                    .Include(ent => ent.Districts)
                    .First(ent => ent.Id == cityId);

                return city;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2500), HttpStatusCode.BadRequest);
                return new City();
            }
        }
        #endregion

    }
}

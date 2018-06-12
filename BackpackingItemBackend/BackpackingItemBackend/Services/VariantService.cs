using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BackpackingItemBackend.Constants;
using BackpackingItemBackend.DataContext;
using BackpackingItemBackend.Models;
using Lib.Web.Services;
using Microsoft.EntityFrameworkCore;

namespace BackpackingItemBackend.Services
{
    public interface IVariantService
    {
        Variant GetById(long id);
    }

    public class VariantService : IVariantService
    {
        private readonly ApplicationDbContext _context;

        private IThrowService throwService;

        #region Constructor
        public VariantService(ApplicationDbContext context, IThrowService throwService)
        {
            _context = context;
            this.throwService = throwService;
        }
        #endregion

        #region GetById
        public Variant GetById (long id)
        {
            try
            {
                var variant = _context.Variants
                    .Include(ent => ent.Product)
                    .Include(ent => ent.Size)
                    .Include(ent => ent.Color)
                    .Include(ent => ent.Images)
                    .First(ent => ent.Id == id);

                return variant;
            }
            catch (InvalidOperationException)
            {
                throwService.ThrowApiException(ErrorsDefine.Find(2300), HttpStatusCode.BadRequest);
                return new Variant();
            }
        }
        #endregion
    }
}

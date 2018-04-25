using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackpackingItemBackend.Models;

namespace BackpackingItemBackend.Models.ReturnModel
{
    public class SizeReturnModel
    {
        #region Id 
        public long Id { get; set; }
        #endregion

        #region Name
        public string Name { get; set; }
        #endregion

        #region Variants
        //public List<Variant> Variants { get; set; }
        #endregion

        public static SizeReturnModel Create(Size size)
        {
            return new SizeReturnModel()
            {
                Id = size.Id,
                Name = size.Name
            };
        }
    }
}

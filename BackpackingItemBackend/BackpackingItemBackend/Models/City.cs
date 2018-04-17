using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class City
    {
        #region Id
        public long Id { get; set; }
        #endregion

        #region Name 
        public string Name { get; set; }
        #endregion

        #region Districts
        public List<District> Districts { get; set; }
        #endregion
    }
}

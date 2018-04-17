using System;
using System.Collections.Generic;
using System.Text;
using BackpackingItemStore.Core.Models.Abstract;

namespace BackpackingItemStore.Core.Models
{
    public class Category : GuidEntityBase
    {
        #region Name
        public string Name { get; set; }

        #endregion
    }
}

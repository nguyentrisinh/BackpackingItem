﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackpackingItemBackend.Models
{
    public class Size
    {
        #region Id 
        public long Id { get; set; }
        #endregion

        #region Name
        public long Name { get; set; }
        #endregion

        #region Weight 
        public string Weight { get; set; }
        #endregion

        #region Variants
        public List<Variant> Variants { get; set; }
        #endregion
    }
}
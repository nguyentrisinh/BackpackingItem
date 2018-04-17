using System;
using System.Collections.Generic;
using System.Text;

namespace BackpackingItemStore.Core.Models.Abstract
{
    public abstract class GuidEntityBase : EntityBase<Guid, long>
    {
        #region Constructors
        public GuidEntityBase()
        {
            this.Id = Guid.NewGuid();
        }
        #endregion
    }
}

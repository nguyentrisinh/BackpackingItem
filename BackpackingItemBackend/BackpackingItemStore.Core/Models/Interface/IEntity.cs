using System;
using System.Collections.Generic;
using System.Text;

namespace BackpackingItemStore.Core.Models.Interface
{
    public interface IEntity<TIdentifier>
    {
        #region Id

        TIdentifier Id { get; set; }

        #endregion
    }
}

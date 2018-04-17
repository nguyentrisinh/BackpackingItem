using System;
using System.Collections.Generic;
using System.Text;

namespace BackpackingItemStore.Core.Models.Interface
{
    public interface IAudit
    {
        DateTime CreatedDate { get; set; }
        DateTime ModifiedDate { get; set; }
        long CreatedAt { get; set; }
        long ModifiedAt { get; set; }
    }
}

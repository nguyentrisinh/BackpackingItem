using System;
using System.Collections.Generic;
using System.Text;

namespace BackpackingItemStore.Core.Extensions
{
    public static class HiResDateTime
    {
        private static DateTime lastTimeStamp = DateTime.UtcNow;
        public static DateTime UtcNow
        {
            get
            {
                DateTime original = lastTimeStamp.AddMilliseconds(1);
                DateTime now = DateTime.UtcNow;
                lastTimeStamp = now > original ? now : original;
                return lastTimeStamp;
            }
        }
    }
}

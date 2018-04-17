using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace BackpackingItemStore.Core.Models
{
    public class ApplicationUser : IdentityUser
    {
        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ApplicationUser()
        {
            this.CreatedDate = new SqlDateTime(DateTime.UtcNow).Value;
            this.ModifiedDate = new SqlDateTime(DateTime.UtcNow).Value;
            this.CreatedAt = (long)(this.CreatedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            this.ModifiedAt = (long)(this.ModifiedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }
        #endregion

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public Status Status { get; set; }

        public DateTime Birthday { get; set; }


        #region SecondaryId
        public long SecondaryId { get; set; }
        #endregion

        #region CreatedDate
        private DateTime mCreatedDate = new SqlDateTime(DateTime.UtcNow).Value;
        [DataType(DataType.Date)]
        public DateTime CreatedDate
        {
            get
            {
                return mCreatedDate;
            }
            set
            {
                mCreatedDate = value;
            }
        }
        #endregion

        #region ModifiedDate
        private DateTime mModifiedDate = new SqlDateTime(DateTime.UtcNow).Value;
        [DataType(DataType.Date)]
        public DateTime ModifiedDate
        {
            get
            {
                return mModifiedDate;
            }
            set
            {
                mModifiedDate = value;
            }
        }

        #region CreatedAt
        public long CreatedAt { get; set; }
        #endregion

        #region ModifiedAt

        public long ModifiedAt { get; set; }
        #endregion

        #endregion
    }

    public enum Status
    {
        Active = 1,
        Inactive = 2
    }

    public enum Gender
    {
        Male = 1,
        Female = 2
    }
}

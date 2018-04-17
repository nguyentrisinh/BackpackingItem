using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using BackpackingItemStore.Core.Models.Interface;

namespace BackpackingItemStore.Core.Models.Abstract
{
    public abstract class EntityBase<TIdentifier, TSecondaryIdentifier> : IEntity<TIdentifier>, IAudit
    {
        [Key]
        #region Id
        public TIdentifier Id
        {
            get;
            set;
        }
        #endregion

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        #region SecondaryId
        public TSecondaryIdentifier SecondaryId { get; set; }
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

        #region Equals
        public override bool Equals(object obj)
        {
            if (obj != null && obj is IEntity<TIdentifier>)
            {
                return (obj as IEntity<TIdentifier>).Id.Equals(this.Id);
            }

            return false;
        }
        #endregion

        #region Get Hash Code
        public override int GetHashCode()
        {
            unchecked
            {
                var result = 0;
                result = (result * 397) ^ this.Id.GetHashCode();

                return result;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor.
        /// </summary>
        public EntityBase()
        {
            this.CreatedDate = new SqlDateTime(DateTime.UtcNow).Value;
            this.ModifiedDate = new SqlDateTime(DateTime.UtcNow).Value;
            this.CreatedAt = (long)(this.CreatedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
            this.ModifiedAt = (long)(this.ModifiedDate.Subtract(new DateTime(1970, 1, 1))).TotalMilliseconds;
        }
        #endregion
    }
}

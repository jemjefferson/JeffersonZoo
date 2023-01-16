using System;

namespace BoothItems
{
    /// <summary>
    /// This class represents a coupon book.
    /// </summary>
    [Serializable]
    public class CouponBook : Item
    {
        /// <summary>
        /// Date the coupon was made.
        /// </summary>
        private DateTime dateMade;

        /// <summary>
        /// Date the coupon expired.
        /// </summary>
        private DateTime dateExpired;

        /// <summary>
        /// Instantiates a new instance of the CouponBook class.
        /// </summary>
        /// <param name="dateMade">Date the coupon was made.</param>
        /// <param name="dateExpired">Date the coupon expired.</param>
        /// <param name="weight">The weight of the couponbook.</param>
        public CouponBook(DateTime dateMade, DateTime dateExpired, double weight)
            : base(weight)
            {
            this.dateMade = dateMade;
            this.dateExpired = dateExpired;
            }

        /// <summary>
        /// Gets the coupon's date made date.
        /// </summary>
        public DateTime DateMade
        {
            get
            {
                return this.dateMade;
            }
        }

        /// <summary>
        /// Gets the coupon's date expired date.
        /// </summary>
        public DateTime DateExpired
        {
            get
            {
                return this.dateExpired;
            }
        }
    }
}

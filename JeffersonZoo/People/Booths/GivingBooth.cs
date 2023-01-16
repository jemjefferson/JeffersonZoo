using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BoothItems;

namespace People
{
    /// <summary>
    /// This class represents a booth that gives items.
    /// </summary>
    [Serializable]
    public class GivingBooth : Booth
    {
        /// <summary>
        /// Instantiates a new instance of the GivingBooth class.
        /// </summary>
        /// <param name="attendant">The attendant working the booth.</param>
        public GivingBooth(Employee attendant)
            : base(attendant)
        {
            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new CouponBook(new System.DateTime(2021, 9, 2), new System.DateTime(2022, 9, 2), 0.8));
            }

            for (int i = 0; i < 10; i++)
            {
                this.Items.Add(new Map(0.5, new System.DateTime(2021, 9, 2)));
            }
        }

        /// <summary>
        /// Gives out a free coupon book to a guest.
        /// </summary>
        /// <returns>Returns a coupon book.</returns>
        public CouponBook GiveFreeCouponBook()
        {
            try
            {
                Item couponBook = this.Attendant.FindItem(this.Items, typeof(CouponBook));
                return couponBook as CouponBook;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Coupon book not found.", ex);
            }
        }

        /// <summary>
        /// Gives out a free map to a guest.
        /// </summary>
        /// <returns>Returns a map.</returns>
        public Map GiveFreeMap()
        {
            try
            {
                Item map = this.Attendant.FindItem(this.Items, typeof(Map));
                return map as Map;
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Map not found.", ex);
            }
        }
    }
}

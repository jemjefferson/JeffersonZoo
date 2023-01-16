using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// This class represents a item that can be sold.
    /// </summary>
    [Serializable]
    public abstract class SoldItem : Item
    {
        /// <summary>
        /// Price of item.
        /// </summary>
        private decimal price;

        /// <summary>
        /// Instantiates a new instance of the SoldItem class.
        /// </summary>
        /// <param name="price">Price of item.</param>
        /// <param name="weight">Weight of item.</param>
        public SoldItem(decimal price, double weight)
            : base(weight)
        {
            this.price = price;
        }

        /// <summary>
        /// Gets the price of the item.
        /// </summary>
        public decimal Price
        {
            get
            {
                return this.price;
            }
        }
    }
}

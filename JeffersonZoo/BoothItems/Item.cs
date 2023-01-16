using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// This class represents different booth items.
    /// </summary>
    [Serializable]
    public abstract class Item
    {
        /// <summary>
        /// The weight of the item.
        /// </summary>
        private double weight;

        /// <summary>
        /// Instantiates a new instance of the Item class.
        /// </summary>
        /// <param name="price">The price of the item.</param>
        /// <param name="weight">The weight of the item.</param>
        public Item(double weight)
        {
            this.weight = weight;
        }

        /// <summary>
        /// Gets the weight of the item.
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// This class represents a water bottle.
    /// </summary>
    [Serializable]
    public class WaterBottle : SoldItem
    {
        /// <summary>
        /// The serial number of the water bottle.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Instantiates a new instance of the water bottle class.
        /// </summary>
        /// <param name="price">The price of the water bottle.</param>
        /// <param name="serialNumber">The serial number of the water bottle.</param>
        /// <param name="weight">The weight of the water bottle.</param>
        public WaterBottle(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets the serial number of the water bottle.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }
    }
}

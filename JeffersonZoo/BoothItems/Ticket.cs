using System;

namespace BoothItems
{
    /// <summary>
    /// This class represents a zoo ticket.
    /// </summary>
    [Serializable]
    public class Ticket : SoldItem
    {
        /// <summary>
        /// Field showing if the ticket was redeemed or not.
        /// </summary>
        private bool isRedeemed;

        /// <summary>
        /// Serial number of the ticket.
        /// </summary>
        private int serialNumber;

        /// <summary>
        /// Instantiates a new instance of the Ticket class.
        /// </summary>
        /// <param name="price">The price of the ticket.</param>
        /// <param name="serialNumber">The serial number of the ticket.</param>
        /// <param name="weight">The weight of the ticket.</param>
        public Ticket(decimal price, int serialNumber, double weight)
            : base(price, weight)
        {
            this.serialNumber = serialNumber;
        }

        /// <summary>
        /// Gets a value indicating whether or not the ticket is redeemed.
        /// </summary>
        public bool IsRedeemed
        {
            get
            {
                return this.isRedeemed;
            }
        }

        /// <summary>
        /// Gets the serial number of the ticket.
        /// </summary>
        public int SerialNumber
        {
            get
            {
                return this.serialNumber;
            }
        }

        /// <summary>
        /// This method represents redeeming the ticket.
        /// </summary>
        public void Redeem()
        {
            this.isRedeemed = true;
        }
    }
}

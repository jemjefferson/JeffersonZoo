using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using BoothItems;
using MoneyCollectors;
using Reproducers;

namespace People
{
    /// <summary>
    /// This class represents a booth that sells items.
    /// </summary>
    [Serializable]
    public class MoneyCollectingBooth : Booth
    {
        /// <summary>
        /// The money box of the booth.
        /// </summary>
        private IMoneyCollector moneyBox;

        /// <summary>
        /// The price of a ticket.
        /// </summary>
        private decimal ticketPrice;

        /// <summary>
        /// The price of a water bottle.
        /// </summary>
        private decimal waterBottlePrice;

        /// <summary>
        /// Stack of tickets.
        /// </summary>
        private Stack<Ticket> ticketStack;

        /// <summary>
        /// Instantiates a new instance of the MoneyCollectingBooth class.
        /// </summary>
        /// <param name="attendant">The attendant working the booth.</param>
        /// <param name="ticketPrice">The price of a ticket.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="moneyBox">Money box of the booth.</param>
        public MoneyCollectingBooth(Employee attendant, decimal ticketPrice, decimal waterBottlePrice, IMoneyCollector moneyBox)
            : base(attendant)
        {
            this.ticketPrice = ticketPrice;
            this.waterBottlePrice = waterBottlePrice;
            this.moneyBox = moneyBox;
            this.ticketStack = new Stack<Ticket>();

            for (int i = 0; i < 5; i++)
            {
               this.ticketStack.Push(new Ticket(this.ticketPrice, i, 0.01));
            }

            for (int i = 0; i < 5; i++)
            {
                this.Items.Add(new WaterBottle(this.WaterBottlePrice, i, 1));
            }
        }

        /// <summary>
        /// Gets the money balance of the booth.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBox.MoneyBalance;
            }
        }

        /// <summary>
        /// Gets the price of a ticket.
        /// </summary>
        public decimal TicketPrice
        {
            get
            {
                return this.ticketPrice;
            }
        }

        /// <summary>
        /// Gets the price of a water bottle.
        /// </summary>
        public decimal WaterBottlePrice
        {
            get
            {
                return this.waterBottlePrice;
            }
        }

        /// <summary>
        /// Adds money to the money box of the booth.
        /// </summary>
        /// <param name="amount">The amount of money to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyBox.AddMoney(amount);
        }

        /// <summary>
        /// Removes money from the money box of the booth.
        /// </summary>
        /// <param name="amount">The amount to be removed.</param>
        /// <returns>Returns the amount of money left.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyBox.RemoveMoney(amount);
        }

        /// <summary>
        /// Sells a ticket to a guest.
        /// </summary>
        /// <param name="payment">The payment for a ticket.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket SellTicket(decimal payment)
        {
            Item ticket = null;

            try
            {
                if (payment >= this.TicketPrice)
                {
                    ticket = this.ticketStack.Pop();

                    if (ticket != null)
                    {
                        this.AddMoney(payment);
                    }
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Ticket not found.", ex);
            }

            return ticket as Ticket;
        }

        /// <summary>
        /// Sells a water bottle to a guest.
        /// </summary>
        /// <param name="payment">The payment for a water bottle.</param>
        /// <returns>Returns a water bottle.</returns>
        public WaterBottle SellWaterBottle(decimal payment)
        {
            Item bottle = null;

            try
            {
                if (payment >= this.WaterBottlePrice)
                {
                    bottle = this.Attendant.FindItem(this.Items, typeof(WaterBottle));
                    this.AddMoney(payment);
                }
            }
            catch (MissingItemException ex)
            {
                throw new NullReferenceException("Water bottle not found.", ex);
            }

            return bottle as WaterBottle;
        }
    }
}

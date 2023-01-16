using System;
using MoneyCollectors;

namespace People
{
    /// <summary>
    /// The class which is used to represent a wallet.
    /// </summary>
    [Serializable]
    public class Wallet : IMoneyCollector
    {
        /// <summary>
        /// The color of the wallet.
        /// </summary>
        private WalletColor color;

        /// <summary>
        /// The compartment of the wallet that holds money.
        /// </summary>
        private IMoneyCollector moneyPocket;

        /// <summary>
        /// Initializes a new instance of the Wallet class.
        /// </summary>
        /// <param name="color">The color of the wallet.</param>
        public Wallet(WalletColor color)
        {
            this.color = color;
            this.moneyPocket = new MoneyPocket();

            this.moneyPocket.OnBalanceChange = () =>
            {
                this.OnBalanceChange?.Invoke();
            };
        }

        /// <summary>
        /// Gets or sets the money balance in the wallet.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyPocket.MoneyBalance;
            }

            set
            {
                this.moneyPocket.MoneyBalance = value;
            }
        }

        /// <summary>
        /// Gets or sets the OnBalanceChange Action.
        /// </summary>
        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Gets or sets the wallet's color.
        /// </summary>
        public WalletColor WalletColor
        {
            get
            {
                return this.color;
            }

            set
            {
                this.color = value;
            }
        }

        /// <summary>
        /// Adds money to the wallet.
        /// </summary>
        /// <param name="amount">Amount of money to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.moneyPocket.AddMoney(amount);
        }

        /// <summary>
        /// Removes money from the wallet.
        /// </summary>
        /// <param name="amount">The amount to be removed.</param>
        /// <returns>Returns the amount left in the wallet after the money has been removed.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.moneyPocket.RemoveMoney(amount);
        }
    }
}
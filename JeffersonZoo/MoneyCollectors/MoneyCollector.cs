using System;

namespace MoneyCollectors
{
    /// <summary>
    /// This class represents a class for things that can collect money.
    /// </summary>
    [Serializable]
    public abstract class MoneyCollector : IMoneyCollector
    {
        /// <summary>
        /// The money balance of the money collector.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Gets the money balance of the money collector.
        /// </summary>
        public decimal MoneyBalance
        {
            get
            {
                return this.moneyBalance;
            }

            set
            {
                this.moneyBalance = value;

                if (this.OnBalanceChange != null)
                {
                    this.OnBalanceChange();
                }
            }
        }

        public Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the money balance of the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// Removes money from the money balance of the money collector.
        /// </summary>
        /// <param name="amount">The amount of money to be removed.</param>
        /// <returns>Returns the remaining balance.</returns>
        public virtual decimal RemoveMoney(decimal amount)
        {
            decimal amountRemoved;

            // If there is enough money in the wallet...
            if (this.MoneyBalance >= amount)
            {
                // Return the requested amount.
                amountRemoved = amount;
            }
            else
            {
                // Otherwise return all the money that is left.
                amountRemoved = this.MoneyBalance;
            }

            // Subtract the amount removed from the wallet's money balance.
            this.MoneyBalance -= amountRemoved;

            return amountRemoved;
        }
    }
}

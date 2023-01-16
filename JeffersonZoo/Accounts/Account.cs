using System;
using MoneyCollectors;

namespace Accounts
{
    /// <summary>
    /// This class represents a money account.
    /// </summary>
    [Serializable]
    public class Account : IMoneyCollector
    {
        /// <summary>
        /// Money balance in the account.
        /// </summary>
        private decimal moneyBalance;

        /// <summary>
        /// Instantiates a new instance of the Account class.
        /// </summary>
        /// <param name="moneyBalance">Money balance in the account.</param>
        public Account()
        {
        }

        /// <summary>
        /// Gets the money balance of the account.
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
        /// Adss money to the account.
        /// </summary>
        /// <param name="amount">Amount to be added.</param>
        public void AddMoney(decimal amount)
        {
            this.MoneyBalance += amount;
        }

        /// <summary>
        /// Removes money from the account.
        /// </summary>
        /// <param name="amount">Amount to be removed.</param>
        /// <returns>Return the remaining amount.</returns>
        public decimal RemoveMoney(decimal amount)
        {
            return this.MoneyBalance -= amount;
        }
    }
}

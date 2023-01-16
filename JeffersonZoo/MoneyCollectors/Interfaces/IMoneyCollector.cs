using System;

namespace MoneyCollectors
{
    public interface IMoneyCollector
    {
        /// <summary>
        /// Gets the money balance.
        /// </summary>
        decimal MoneyBalance { get; set; }

        Action OnBalanceChange { get; set; }

        /// <summary>
        /// Adds money to the money balance.
        /// </summary>
        /// <param name="amount">The amount to be added.</param>
        void AddMoney(decimal amount);

        /// <summary>
        /// Removes money from the money balance.
        /// </summary>
        /// <param name="amount">The amount to be removed.</param>
        /// <returns></returns>
        decimal RemoveMoney(decimal amount);
    }
}

using System;

namespace MoneyCollectors
{
    /// <summary>
    /// This class represents a "Money Pocket".
    /// </summary>
    [Serializable]
    public class MoneyPocket : MoneyCollector
    {
        /// <summary>
        /// Removes money from the money pocket.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unfold();
            decimal remainingBalance = base.RemoveMoney(amount);
            this.Fold();

            return remainingBalance;
            
        }

        /// <summary>
        /// Folds the money pocket.
        /// </summary>
        private void Fold()
        {
        }

        /// <summary>
        /// Unfoldds the money pokcet.
        /// </summary>
        private void Unfold()
        {
        }
    }
}

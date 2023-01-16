using System;

namespace MoneyCollectors
{
    /// <summary>
    /// This class represents a "Money Box'
    /// </summary>
    [Serializable]
    public class MoneyBox : MoneyCollector
    {
        /// <summary>
        /// Removes money from the money balance of the money box.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public override decimal RemoveMoney(decimal amount)
        {
            this.Unlock();
            decimal remainingBalance = base.RemoveMoney(amount);
            this.Lock();

            return remainingBalance;
        }

        /// <summary>
        /// Locks the money box.
        /// </summary>
        private void Lock()
        {
        }

        /// <summary>
        /// Unlocks the money box.
        /// </summary>
        private void Unlock()
        {
        }
    }
}

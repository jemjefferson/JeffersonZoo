using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// This class represents eating behavior of animals.
    /// </summary>
    [Serializable]
    public class ConsumeBehavior : IEatBehavior
    {
        /// <summary>
        /// The eater eats.
        /// </summary>
        /// <param name="eater">The eater eating.</param>
        /// <param name="food">The food being consumed.</param>
        public void Eat(IEater eater, Food food)
        {
            // Increase animal's weight as a result of eating food.
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);
        }
    }
}

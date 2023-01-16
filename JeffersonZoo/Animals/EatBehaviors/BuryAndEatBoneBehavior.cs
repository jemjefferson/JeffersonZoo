using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of burying and eating a bone.
    /// </summary>
    [Serializable]
    public class BuryAndEatBoneBehavior : IEatBehavior
    {
        /// <summary>
        /// The eater eats.
        /// </summary>
        /// <param name="eater">The eater eating.</param>
        /// <param name="food">The food being consumed.</param>
        public void Eat(IEater eater, Food food)
        {
            this.BuryBone(food);
            this.DigUpAndEatBone();

            // Increase animal's weight as a result of eating food.
            eater.Weight += food.Weight * (eater.WeightGainPercentage / 100);

            this.Bark();
        }

        /// <summary>
        /// The eater barks.
        /// </summary>
        private void Bark()
        {
        }

        /// <summary>
        /// The eater buries a bone.
        /// </summary>
        /// <param name="bone">The bone being buried.</param>
        private void BuryBone(Food bone)
        {
        }

        /// <summary>
        /// The eater digs up the bone and eats it.
        /// </summary>
        private void DigUpAndEatBone()
        {
        }
    }
}

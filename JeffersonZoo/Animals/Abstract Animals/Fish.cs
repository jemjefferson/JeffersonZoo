using System;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// This class represents a fish.
    /// </summary>
    [Serializable]
    public abstract class Fish : Animal
    {
        /// <summary>
        /// Instantiates a new instance of the Fish class.
        /// </summary>
        /// <param name="name">The name of the fish.</param>
        /// <param name="age">The age of the fish.</param>
        /// <param name="weight">The weight of the fish.</param>
        /// <param name="gender">The fish's gender.</param>
        public Fish(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);
            this.EatBehavior = new ConsumeBehavior();
            this.ReproduceBehavior = new LayEggBehavior();
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public override double WeightGainPercentage
        {
            get
            {
                return 5.0;
            }
        }
    }
}

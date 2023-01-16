using System;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent a platypus.
    /// </summary>
    [Serializable]
    public sealed class Platypus : Mammal, IHatchable
    {
        /// <summary>
        /// Initializes a new instance of the Platypus class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The Platypus's gender.</param>
        public Platypus(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 12.0;

            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Swim);
            this.EatBehavior = new ShowAffectionBehavior();
            this.ReproduceBehavior = new LayEggBehavior();
        }

        /// <summary>
        /// Gets the display size of the animal.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                double displaySize = 0;
                displaySize = (this.Age == 0) ? 0.5 : 1.1;
                return displaySize;
            }
        }

        /// <summary>
        /// Hatches from its egg.
        /// </summary>
        public void Hatch()
        {
            // Break out of egg.
        }
    }
}
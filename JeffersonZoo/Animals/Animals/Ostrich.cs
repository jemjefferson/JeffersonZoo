using System;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// This class represents an Ostrich.
    /// </summary>
    [Serializable]
    public sealed class Ostrich : Bird
    {
        /// <summary>
        /// Instantiates a new instance of the Ostrich class.
        /// </summary>
        /// <param name="name">The name of the ostrich.</param>
        /// <param name="age">The age of the ostrich.</param>
        /// <param name="weight">The weight of the ostrich.</param>
        /// <param name="gender">The Ostrich's gender.</param>
        public Ostrich(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 30.0;

            this.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior(MoveBehaviorType.Pace);
        }

        /// <summary>
        /// Gets the display size of the animal.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                double displaySize = 0;
                displaySize = (this.Age == 0) ? 0.4 : 0.8;
                return displaySize;
            }
        }
    }
}

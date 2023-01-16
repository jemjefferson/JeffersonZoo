using System;
using CagedItems;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This class represents a shark.
    /// </summary>
    [Serializable]
    public class Shark : Fish
    {
        /// <summary>
        /// Instantiates a new instance of the Shark class.
        /// </summary>
        /// <param name="name">The name of the shark.</param>
        /// <param name="age">The age of the shark.</param>
        /// <param name="weight">The weight of the shark.</param>
        /// <param name="gender">The Shark's gender.</param>
        public Shark(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 18.0;
        }

        /// <summary>
        /// Gets the display size of the animal.
        /// </summary>
        public override double DisplaySize
        {
            get
            {
                double displaySize = 0;
                displaySize = (this.Age == 0) ? 1 : 1.5;
                return displaySize;
            }
        }

        /// <summary>
        /// Gets the weight gain percentage.
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

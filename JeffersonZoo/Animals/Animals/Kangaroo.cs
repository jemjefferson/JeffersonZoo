using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This class represents a kangaroo.
    /// </summary>
    [Serializable]
    public class Kangaroo : Mammal
    {
        /// <summary>
        /// Instantiates a new instance of the Kangaroo class.
        /// </summary>
        /// <param name="name">The name of the kangaroo.</param>
        /// <param name="age">The age of the kangaroo.</param>
        /// <param name="weight">The weight of the kangaroo.</param>
        /// <param name="gender">The Kangaroo's gender.</param>
        public Kangaroo(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 13.0;
        }
    }
}

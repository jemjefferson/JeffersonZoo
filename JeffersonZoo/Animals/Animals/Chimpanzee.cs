using System;
using MoneyCollectors;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This class represents a chimpanzee.
    /// </summary>
    [Serializable]
    public class Chimpanzee : Mammal
    {
        /// <summary>
        /// The class represents a chimpanzee.
        /// </summary>
        /// <param name="name">The name of the chimpanzee.</param>
        /// <param name="age">The age of the chimpanzee.</param>
        /// <param name="weight">The weight of the chimpanzee.</param>
        /// <param name="gender">The Chimpanzee's gender.</param>
        public Chimpanzee(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 10.0;
        }
    }
}

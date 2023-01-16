using System;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This class represents an eagle.
    /// </summary>
    [Serializable]
    public class Eagle : Bird
    {
        /// <summary>
        /// Instantiates a new instance of the Eagle class.
        /// </summary>
        /// <param name="name">Name of the eagle.</param>
        /// <param name="age">Age of the eagle.</param>
        /// <param name="weight">Weight of the eagle.</param>
        /// <param name="gender">The eagle's gender.</param>
        public Eagle(string name, int age, double weight, Gender gender)
            : base(name, age, weight, gender)
        {
            this.BabyWeightPercentage = 25.0;
        }
    }
}
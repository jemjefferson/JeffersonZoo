using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The class represents the behavior of standing still.
    /// </summary>
    [Serializable]
    public class NoMoveBehavior : IMoveBehavior
    {
        /// <summary>
        /// The animal stands still.
        /// </summary>
        /// <param name="animal">The animal standing still.</param>
        public void Move(Animal animal)
        {
            // the animal is standing still.
        }
    }
}

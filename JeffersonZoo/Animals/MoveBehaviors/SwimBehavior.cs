using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of swimming.
    /// </summary>
    [Serializable]
    public class SwimBehavior : IMoveBehavior
    {
        /// <summary>
        /// The animal swims.
        /// </summary>
        /// <param name="animal">The animal that is swimming.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
            MoveHelper.MoveVertically(animal, animal.MoveDistance);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of pacing.
    /// </summary>
    [Serializable]
    public class PaceBehavior : IMoveBehavior
    {
        /// <summary>
        /// The animal paces.
        /// </summary>
        /// <param name="animal">The animal pacing.</param>
        public void Move(Animal animal)
        {
            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
        }
    }
}

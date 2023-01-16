using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of flying.
    /// </summary>
    [Serializable]
    public class FlyBehavior : IMoveBehavior
    {
        /// <summary>
        /// The animal flies.
        /// </summary>
        /// <param name="animal">The animal flying.</param>
        public void Move(Animal animal)
        {
            // Fly.
            int flyPace = 10;

            // if the animal is moving up...
            if (animal.YDirection == VerticalDirection.Up)
            {
                // Move the animal down.
                animal.YPosition += flyPace;
                animal.YDirection = VerticalDirection.Down;
            }
            else
            {
                // Move the animal to the up.
                animal.YPosition -= flyPace;
                animal.YDirection = VerticalDirection.Up;
            }

            MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
        }
    }
}

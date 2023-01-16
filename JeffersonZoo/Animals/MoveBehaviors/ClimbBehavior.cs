using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of climbing.
    /// </summary>
    [Serializable]
    public class ClimbBehavior : IMoveBehavior
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        private int maxHeight;

        private ClimbProcess process;

        /// <summary>
        /// The animal climbs.
        /// </summary>
        /// <param name="animal">The animal climbing.</param>
        public void Move(Animal animal)
        {
            switch (this.process)
            {
                // if the animal is climbing
                // make sure it's moving up (vertical direction)
                // move vertically
                // if the animal hit the maximum height (top of cage or other)
                // switch to falling
                case ClimbProcess.Climbing:

                    animal.YDirection = VerticalDirection.Up;

                    MoveHelper.MoveVertically(animal, animal.MoveDistance);

                    if (animal.YPosition - animal.MoveDistance <= this.maxHeight)
                    {
                        animal.YDirection = VerticalDirection.Down;

                        if (animal.XDirection == HorizontalDirection.Left)
                        {
                            animal.XDirection = HorizontalDirection.Right;
                        }
                        else
                        {
                            animal.XDirection = HorizontalDirection.Left;
                        }

                        this.NextProcess(animal);
                    }

                    break;

                // if the animal is falling
                // move diagonally at a relatively steep angle
                // if the animal hit the bottom of the cage
                // switch to scurrying
                case ClimbProcess.Falling:

                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);
                    MoveHelper.MoveVertically(animal, animal.MoveDistance * 2);

                    if (animal.YPosition + animal.MoveDistance >= animal.YPositionMax)
                    {
                        this.NextProcess(animal);
                    }

                    break;

                // if the animal is scurrying
                // move horizontally along the bottom of the cage
                // if the animal hit a vertical edge
                // set the random max height
                // switch to climbing
                case ClimbProcess.Scurrying:

                    MoveHelper.MoveHorizontally(animal, animal.MoveDistance);

                    if (animal.XPosition - animal.MoveDistance <= 0 || animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                    {
                        if (animal.XPosition + animal.MoveDistance >= animal.XPositionMax)
                        {
                            animal.XPosition = animal.XPositionMax;
                        }
                        else
                        {
                            animal.XPosition = 0;
                        }

                        this.NextProcess(animal);
                    }

                    break;
            }
        }

        private void NextProcess(Animal animal)
        {
            switch (this.process)
            {
                case ClimbProcess.Climbing:
                    this.process = ClimbProcess.Falling;
                    break;
                case ClimbProcess.Falling:
                    this.process = ClimbProcess.Scurrying;
                    break;
                case ClimbProcess.Scurrying:
                    int higherMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.15));
                    int lowerMax = Convert.ToInt32(Math.Floor(Convert.ToDouble(animal.YPositionMax) * 0.85));

                    this.maxHeight = ClimbBehavior.random.Next(higherMax, lowerMax + 1);

                    this.process = ClimbProcess.Climbing;
                    break;
            }
        }
    }
}

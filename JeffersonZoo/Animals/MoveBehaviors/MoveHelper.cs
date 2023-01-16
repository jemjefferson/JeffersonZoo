using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// This class holds the move code for animals.
    /// </summary>
    public static class MoveHelper
    {
        /// <summary>
        /// Animal moves horizontally.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        /// <param name="moveDistance">The distance the animal moves.</param>
        public static void MoveHorizontally(Animal animal, int moveDistance)
        {
          switch (animal.HungerState)
          {
                case CagedItems.HungerState.Satisfied:

                // if the animal is moving to the right...
            if (animal.XDirection == HorizontalDirection.Right)
            {
                // if the animal's current vertiacal position plus the move distance the animal is going to move is greater than the vertical max position of the animal...
                if (animal.XPosition + moveDistance > animal.XPositionMax)
                {
                    // Stop the animal and turn it around.
                    animal.XPosition = animal.XPositionMax;
                    animal.XDirection = HorizontalDirection.Left;
                }
                else
                {
                    // Move the animal to the right.
                    animal.XPosition += moveDistance;
                }
            }
            else
            {
                // if the animal's current vertical position minus the move distance is less then 0...
                if (animal.XPosition - moveDistance < 0)
                {
                    // Sto pthe animal and turn it around.
                    animal.XPosition = 0;
                    animal.XDirection = HorizontalDirection.Right;
                }
                else
                {
                    // Move the animal to the left.
                    animal.XPosition -= moveDistance;
                }
            }

            break;

                case CagedItems.HungerState.Hungry:

            moveDistance = moveDistance / 4;

                    // if the animal is moving to the right...
            if (animal.XDirection == HorizontalDirection.Right)
                    {
                        // if the animal's current vertiacal position plus the move distance the animal is going to move is greater than the vertical max position of the animal...
                        if (animal.XPosition + moveDistance > animal.XPositionMax)
                        {
                            // Stop the animal and turn it around.
                            animal.XPosition = animal.XPositionMax;
                            animal.XDirection = HorizontalDirection.Left;
                        }
                        else
                        {
                            // Move the animal to the right.
                            animal.XPosition += moveDistance;
                        }
                    }
                    else
                    {
                        // if the animal's current vertical position minus the move distance is less then 0...
                        if (animal.XPosition - moveDistance < 0)
                        {
                            // Sto pthe animal and turn it around.
                            animal.XPosition = 0;
                            animal.XDirection = HorizontalDirection.Right;
                        }
                        else
                        {
                            // Move the animal to the left.
                            animal.XPosition -= moveDistance;
                        }
                    }

            break;

                case CagedItems.HungerState.Starving:
            moveDistance = 0;
            break;

                case CagedItems.HungerState.Tired:
            moveDistance = 0;
            break;

                default:
            break;
          }
        }

        /// <summary>
        /// Animal moves vertically.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        /// <param name="moveDistance">The distance the animal moves.</param>
        public static void MoveVertically(Animal animal, int moveDistance)
        {
            switch (animal.HungerState)
            {
                case CagedItems.HungerState.Satisfied:

                    // if the animal is moving up...
                    if (animal.YDirection == VerticalDirection.Down)
                    {
                        // if the animal's current vertiacal position plus the move distance the animal is going to move is greater than the vertical max position of the animal...
                        if (animal.YPosition + moveDistance > animal.YPositionMax)
                        {
                            // Stop the animal and turn it around.
                            animal.YPosition = animal.YPositionMax;
                            animal.YDirection = VerticalDirection.Up;
                        }
                        else
                        {
                            // Move the animal to the up.
                            animal.YPosition += moveDistance;
                        }
                    }
                    else
                    {
                        // if the animal's current vertical position minus the move distance is less then 0...
                        if (animal.YPosition + moveDistance < 0)
                        {
                            // Sto pthe animal and turn it around.
                            animal.YPosition = 0;
                            animal.YDirection = VerticalDirection.Down;
                        }
                        else
                        {
                            // Move the animal to the down.
                            animal.YPosition -= moveDistance;
                        }
                    }

                    break;

                case CagedItems.HungerState.Hungry:

                    moveDistance = moveDistance / 4;

                    // if the animal is moving up...
                    if (animal.YDirection == VerticalDirection.Down)
                    {
                        // if the animal's current vertiacal position plus the move distance the animal is going to move is greater than the vertical max position of the animal...
                        if (animal.YPosition + moveDistance > animal.YPositionMax)
                        {
                            // Stop the animal and turn it around.
                            animal.YPosition = animal.YPositionMax;
                            animal.YDirection = VerticalDirection.Up;
                        }
                        else
                        {
                            // Move the animal to the up.
                            animal.YPosition += moveDistance;
                        }
                    }
                    else
                    {
                        // if the animal's current vertical position minus the move distance is less then 0...
                        if (animal.YPosition + moveDistance < 0)
                        {
                            // Sto pthe animal and turn it around.
                            animal.YPosition = 0;
                            animal.YDirection = VerticalDirection.Down;
                        }
                        else
                        {
                            // Move the animal to the down.
                            animal.YPosition -= moveDistance;
                        }
                    }

                    break;

                case CagedItems.HungerState.Starving:
                    moveDistance = 0;
                    break;
                case CagedItems.HungerState.Tired:
                    moveDistance = 0;
                    break;
                default:
                    break;
            }
        }
    }
}

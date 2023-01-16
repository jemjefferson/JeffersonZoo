using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of hovering.
    /// </summary>
    [Serializable]
    public class HoverBehavior : IMoveBehavior
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        private int stepCount;

        private HoverProcess process;

        /// <summary>
        /// The animal hovers.
        /// </summary>
        /// <param name="animal">The animal hovering.</param>
        public void Move(Animal animal)
        {
            if (this.stepCount == 0)
            {
                this.NextProcess(animal);
            }

            this.stepCount--;

            int moveDistance;

            if (this.process == HoverProcess.Hovering)
            {
                moveDistance = animal.MoveDistance;

                animal.XDirection = HoverBehavior.random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = HoverBehavior.random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                moveDistance = animal.MoveDistance * 4;
            }

            MoveHelper.MoveHorizontally(animal, moveDistance);
            MoveHelper.MoveVertically(animal, moveDistance);
        }

        private void NextProcess(Animal animal)
        {
            if (this.process == HoverProcess.Hovering)
            {
                this.process = HoverProcess.Zooming;

                this.stepCount = HoverBehavior.random.Next(5, 9);

                animal.XDirection = HoverBehavior.random.Next(0, 2) == 0 ? HorizontalDirection.Left : HorizontalDirection.Right;
                animal.YDirection = HoverBehavior.random.Next(0, 2) == 0 ? VerticalDirection.Up : VerticalDirection.Down;
            }
            else
            {
                this.process = HoverProcess.Hovering;

                this.stepCount = HoverBehavior.random.Next(7, 11);
            }
        }
    }
}

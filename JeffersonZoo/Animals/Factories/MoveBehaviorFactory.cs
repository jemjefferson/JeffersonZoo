using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// This class is a factory of move behaviors.
    /// </summary>
    public static class MoveBehaviorFactory
    {
        /// <summary>
        /// Creates movement behavior.
        /// </summary>
        /// <param name="type">The type of movement behavior.</param>
        /// <returns>The created movement behavior.</returns>
        public static IMoveBehavior CreateMoveBehavior(MoveBehaviorType type)
        {
            switch (type)
            {
                case MoveBehaviorType.Fly:
                    return new FlyBehavior();
                    break;
                case MoveBehaviorType.Pace:
                    return new PaceBehavior();
                    break;
                case MoveBehaviorType.Swim:
                    return new SwimBehavior();
                    break;
                case MoveBehaviorType.NoMove:
                    return new NoMoveBehavior();
                    break;
                case MoveBehaviorType.Climb:
                    return new ClimbBehavior();
                    break;
                case MoveBehaviorType.Hover:
                    return new HoverBehavior();
                default:
                    return null;
                    break;
            }
        }
    }
}

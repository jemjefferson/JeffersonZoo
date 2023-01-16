using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foods;

namespace Animals
{
    /// <summary>
    /// This interface represents eating behavior.
    /// </summary>
    public interface IEatBehavior
    {
        /// <summary>
        /// The eater eats.
        /// </summary>
        /// <param name="eater">The eater that will be eating.</param>
        /// <param name="food">The food being consumed.</param>
        void Eat(IEater eater, Food food);
    }
}

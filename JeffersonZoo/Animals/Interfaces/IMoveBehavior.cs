using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    /// <summary>
    /// The interface represents an animal moving.
    /// </summary>
    public interface IMoveBehavior
    {
        /// <summary>
        /// The animal moves.
        /// </summary>
        /// <param name="animal">The animal moving.</param>
        void Move(Animal animal);
    }
}

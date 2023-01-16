using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This interface represents Reproducing behavior.
    /// </summary>
    public interface IReproduceBehavior
    {
        /// <summary>
        /// The mother animal reproduces.
        /// </summary>
        /// <param name="mother">The mother animal.</param>
        /// <param name="baby">The baby being birthed.</param>
        /// <returns>Returns the newborn.</returns>
        IReproducer Reproduce(Animal mother, Animal baby);
    }
}

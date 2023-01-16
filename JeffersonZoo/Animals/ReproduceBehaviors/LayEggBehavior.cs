using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This class represents the behavior of laying an egg.
    /// </summary>
    [Serializable]
    public class LayEggBehavior : IReproduceBehavior
    {
        /// <summary>
        /// The mother animal reproduces.
        /// </summary>
        /// <param name="mother">The mother animal.</param>
        /// <param name="baby">The baby being birthed.</param>
        /// <returns>Returns the newborn.</returns>
        public IReproducer Reproduce(Animal mother, Animal baby)
        {
            // If the baby is hatchable...
            if (baby is IHatchable)
            {
                this.LayEgg(mother, baby);

                // Hatch the baby out of its egg.
                this.HatchEgg(baby as IHatchable);
            }

            // Return the (hatched) baby.
            return baby;
        }

        /// <summary>
        /// Hatches an egg.
        /// </summary>
        /// <param name="egg">The egg to hatch.</param>
        private void HatchEgg(IHatchable egg)
        {
            // Hatch the egg.
            egg.Hatch();
        }

        /// <summary>
        /// Lays an egg.
        /// </summary>
        private void LayEgg(Animal mother, Animal baby)
        {
            mother.Weight -= baby.Weight * 1.25;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;

namespace Zoos
{
    /// <summary>
    /// Result of sorting.
    /// </summary>
    [Serializable]
    public class SortResult
    {
        /// <summary>
        /// Gets or sets a list of animals.
        /// </summary>
        public List<object> Objects { get; set; }

        /// <summary>
        /// Gets or sets the Compare Count.
        /// </summary>
        public int CompareCount { get; set; }

        /// <summary>
        /// Gets or sets the elasped milliseconds.
        /// </summary>
        public double ElapsedMilliseconds { get; set; }

        /// <summary>
        /// Gets or sets the swap count.
        /// </summary>
        public int SwapCount { get; set; }
    }
}

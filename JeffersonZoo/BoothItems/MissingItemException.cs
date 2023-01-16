using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoothItems
{
    /// <summary>
    /// Custom exception for missing booth items.
    /// </summary>
    public class MissingItemException : Exception
    {
        /// <summary>
        /// Instantiates a new instance of the MissingItemException class.
        /// </summary>
        /// <param name="message">Message to display when there is an exception.</param>
        public MissingItemException(string message)
            : base(message)
        {
        }
    }
}

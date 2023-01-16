using System;

namespace BoothItems
{
    /// <summary>
    /// This class represents the zoo's map.
    /// </summary>
    [Serializable]
    public class Map : Item
    {
        /// <summary>
        /// This date the map was issued.
        /// </summary>
        private DateTime dateIssued;

        /// <summary>
        /// Instantiates a new instance of the map class.
        /// </summary>
        /// <param name="weight">The weight of the map.</param>
        /// <param name="dateIssued">The date the map was issued.</param>
        public Map(double weight, DateTime dateIssued)
            : base(weight)
        {
            this.dateIssued = dateIssued;
        }

        /// <summary>
        /// Gets the date the map was issued.
        /// </summary>
        public DateTime DateIssued
        {
            get
            {
                return this.dateIssued;
            }
        }
    }
}

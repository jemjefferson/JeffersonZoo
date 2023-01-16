using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using CagedItems;

namespace Zoos
{
    /// <summary>
    /// This class represents the cage that the zoo animals will live in.
    /// </summary>
    [Serializable]
    public class Cage
    {
        private List<ICageable> cagedItems;

        private Action<ICageable> onImageUpdate;

        /// <summary>
        /// Instantiates a new instance of the cage class.
        /// </summary>
        /// <param name="height">Height of the cage.</param>
        /// <param name="width">Width of the cage.</param>
        /// <param name="animalType">Type of the animal(s) in the cage.</param>
        public Cage(int height, int width)
        {
            this.cagedItems = new List<ICageable>();
            this.Height = height;
            this.Width = width;
        }

        /// <summary>
        /// Gets the cage's height.
        /// </summary>
        public int Height { get; private set; }

        /// <summary>
        /// Gets the cage's width.
        /// </summary>
        public int Width { get; private set; }

        /// <summary>
        /// Gets the list of cageables.
        /// </summary>
        public IEnumerable<ICageable> CagedItems
        {
            get
            {
                return this.cagedItems;
            }
        }

        /// <summary>
        /// Gets or sets the onimage update.
        /// </summary>
        public Action<ICageable> OnImageUpdate
        {
            get
            {
                return this.onImageUpdate;
            }

            set
            {
                this.onImageUpdate = value;
            }
        }

        /// <summary>
        /// Adds the caged item to the caged items list.
        /// </summary>
        /// <param name="cagedItem">The item to be added to the list.</param>
        public void Add(ICageable cagedItem)
        {
            this.cagedItems.Add(cagedItem);

            cagedItem.OnImageUpdate += this.HandleImageUpdate;

            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(cagedItem);
            }
        }

        /// <summary>
        /// Removes the caged item from the caged items list.
        /// </summary>
        /// <param name="cagedItem">The item to be removed from the list.</param>
        public void Remove(ICageable cagedItem)
        {
            this.cagedItems.Remove(cagedItem);

            cagedItem.OnImageUpdate -= this.HandleImageUpdate;

            this.OnImageUpdate?.Invoke(cagedItem);
        }

        /// <summary>
        /// Overrides the string appearance of the cage.
        /// </summary>
        /// <returns>Returns the new string format.</returns>
        public override string ToString()
        {
            string result = $"{this.cagedItems[0].GetType()} cage ({this.Width} X {this.Height})";

            foreach (ICageable c in this.cagedItems)
            {
                result = result + $"{Environment.NewLine}{c} ({c.XPosition} x {c.YPosition})";
            }

            return result;
        }

        private void HandleImageUpdate(ICageable item)
        {
            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(item);
            }
        }
    }
}

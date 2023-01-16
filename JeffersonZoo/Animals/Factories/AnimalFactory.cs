using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reproducers;

namespace Animals
{
    /// <summary>
    /// This class represents a class to create animals.
    /// </summary>
    public static class AnimalFactory
    {
        /// <summary>
        /// Creates the animal.
        /// </summary>
        /// <param name="type">Type of animal.</param>
        /// <param name="name">Name of animal.</param>
        /// <param name="age">Age of animal.</param>
        /// <param name="weight">Weight of animal.</param>
        /// <param name="gender">Gender of animal.</param>
        /// <returns>Returns the created animal.</returns>
        public static Animal CreateAnimal(AnimalType type, string name, int age, double weight, Gender gender)
        {
            Animal result = null;

            switch (type)
            {
                case AnimalType.Chimpanzee:
                    result = new Chimpanzee(name, age, weight, gender);
                    break;
                case AnimalType.Dingo:
                    result = new Dingo(name, age, weight, gender);
                    break;
                case AnimalType.Eagle:
                    result = new Eagle(name, age, weight, gender);
                    break;
                case AnimalType.Hummingbird:
                    result = new Hummingbird(name, age, weight, gender);
                    break;
                case AnimalType.Kangaroo:
                    result = new Kangaroo(name, age, weight, gender);
                    break;
                case AnimalType.Ostrich:
                    result = new Ostrich(name, age, weight, gender);
                    break;
                case AnimalType.Platypus:
                    result = new Platypus(name, age, weight, gender);
                    break;
                case AnimalType.Shark:
                    result = new Shark(name, age, weight, gender);
                    break;
                case AnimalType.Squirrel:
                    result = new Squirrel(name, age, weight, gender);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}

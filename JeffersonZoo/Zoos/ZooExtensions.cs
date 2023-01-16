using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Animals;
using People;
using Reproducers;

namespace Zoos
{
    /// <summary>
    /// Extension methods for the zoo.
    /// </summary>
    public static class ZooExtensions
    {
        /// <summary>
        /// Finds an animal based on name.
        /// </summary>
        /// <param name="zoo">The zoo to find the animal in.</param>
        /// <param name="match">The name of the animal to find.</param>
        /// <returns>The first matching animal.</returns>
        public static Animal FindAnimal(this Zoo zoo, Predicate<Animal> match)
        {
            return zoo.Animals.ToList().Find(match);
        }

        /// <summary>
        /// Finds a guest based on name.
        /// </summary>
        /// <param name="zoo">The zoo to find the guest in.</param>
        /// <param name="match">The name of the guest to find.</param>
        /// <returns>The first matching guest.</returns>
        public static Guest FindGuest(this Zoo zoo, Predicate<Guest> match)
        {
            return zoo.Guests.ToList().Find(match);
        }

        /// <summary>
        /// Gets a list of young guests.
        /// </summary>
        /// <param name="zoo">The zoo the guests are in.</param>
        /// <returns>The list of young guests.</returns>
        public static IEnumerable<object> GetYoungGuests(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                where g.Age <= 10
                select new { g.Name, g.Age };
        }

        /// <summary>
        /// Gets a list of female dingoes.
        /// </summary>
        /// <param name="zoo">The zoo the animals are in.</param>
        /// <returns>The list of female dingoes.</returns>
        public static IEnumerable<object> GetFemaleDingoes(this Zoo zoo)
        {
            return
                from d in zoo.Animals
                where d.GetType() == typeof(Dingo) && d.Gender == Gender.Female
                select new { d.Name, d.Age, d.Weight, d.Gender };
        }

        /// <summary>
        /// Gets a list of heavy weight animals.
        /// </summary>
        /// <param name="zoo">The zoo the animals are in.</param>
        /// <returns>The list with heavy animals.</returns>
        public static IEnumerable<object> GetHeavyAnimals(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                where a.Weight > 200
                select new { Type = a.GetType().Name, a.Name, a.Age, a.Weight };
        }

        /// <summary>
        /// Gets list of guests by age.
        /// </summary>
        /// <param name="zoo">The zoo where the guests are.</param>
        /// <returns>The list of guests by age.</returns>
        public static IEnumerable<object> GetGuestsByAge(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                orderby g.Age
                select new { g.Name, g.Age, g.Gender };
        }

        /// <summary>
        /// Gets list of flying animals.
        /// </summary>
        /// <param name="zoo">The zoo holding the animals.</param>
        /// <returns>The list of flying animals.</returns>
        public static IEnumerable<object> GetFlyingAnimals(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                where a.MoveBehavior is FlyBehavior
                select new { AnimalType = a.GetType().Name, a.Name };
        }

        /// <summary>
        /// Gets list of adopted animals.
        /// </summary>
        /// <param name="zoo">The zoo where the animals are.</param>
        /// <returns>The list of adopted animals.</returns>
        public static IEnumerable<object> GetAdoptedAnimals(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                where g.AdoptedAnimal != null
                select new { g.Name,  AnimalName = g.AdoptedAnimal.Name, AnimalType = g.AdoptedAnimal.GetType().Name };
        }

        /// <summary>
        /// Gets the total balance by using the guest's wallet color.
        /// </summary>
        /// <param name="zoo">The zoo the guest is in.</param>
        /// <returns>The total balance.</returns>
        public static IEnumerable<object> GetTotalBalanceByWalletColor(this Zoo zoo)
        {
            return
                from g in zoo.Guests
                group g by g.Wallet.WalletColor.ToString() into gr
                orderby gr.Key
                select new { gr.Key, TotalMoneyBalance = gr.Sum(g => g.Wallet.MoneyBalance) };
        }

        /// <summary>
        /// Gets the average weight by animal type.
        /// </summary>
        /// <param name="zoo">The zoo the animals are in.</param>
        /// <returns>The average weight.</returns>
        public static IEnumerable<object> GetAverageWeightByAnimalType(this Zoo zoo)
        {
            return
                from a in zoo.Animals
                group a by a.GetType().Name into ar
                orderby ar.Key
                select new { AnimalType = ar.Key, AverageWeight = ar.Average(a => a.Weight) };
        }
    }
}

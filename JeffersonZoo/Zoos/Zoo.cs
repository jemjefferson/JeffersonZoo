using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using MoneyCollectors;
using People;
using Reproducers;
using VendingMachines;

namespace Zoos
{
    /// <summary>
    /// The class which is used to represent a zoo.
    /// </summary>
    [Serializable]
    public class Zoo
    {
        /// <summary>
        /// A list of all animals currently residing within the zoo.
        /// </summary>
        private List<Animal> animals;

        /// <summary>
        /// The zoo's vending machine which allows guests to buy snacks for animals.
        /// </summary>
        private VendingMachine animalSnackMachine;

        /// <summary>
        /// The zoo's room for birthing animals.
        /// </summary>
        private BirthingRoom b168;

        /// <summary>
        /// The maximum number of guests the zoo can accommodate at a given time.
        /// </summary>
        private int capacity;

        /// <summary>
        /// A list of all guests currently visiting the zoo.
        /// </summary>
        private List<Guest> guests;

        /// <summary>
        /// The information booth of the zoo.
        /// </summary>
        private GivingBooth informationBooth;

        /// <summary>
        /// The zoo's ladies' restroom.
        /// </summary>
        private Restroom ladiesRoom;

        /// <summary>
        /// The zoo's men's restroom.
        /// </summary>
        private Restroom mensRoom;

        /// <summary>
        /// The name of the zoo.
        /// </summary>
        private string name;

        [NonSerialized]
        private Action<double, double> onBirthingRoomTemperature;

        [NonSerialized]
        private Action<Guest> onAddGuest;

        [NonSerialized]
        private Action<Guest> onRemoveGuest;

        [NonSerialized]
        private Action<Animal> onAddAnimal;

        [NonSerialized]
        private Action<Animal> onRemoveAnimal;

        /// <summary>
        /// The zoo's ticket booth.
        /// </summary>
        private MoneyCollectingBooth ticketBooth;

        /// <summary>
        /// Dictionary of animal cages.
        /// </summary>
        private Dictionary<Type, Cage> cages;

        /// <summary>
        /// Initializes a new instance of the Zoo class.
        /// </summary>
        /// <param name="name">The name of the zoo.</param>
        /// <param name="capacity">The maximum number of guests the zoo can accommodate at a given time.</param>
        /// <param name="restroomCapacity">The capacity of the zoo's restrooms.</param>
        /// <param name="animalFoodPrice">The price of a pound of food from the zoo's animal snack machine.</param>
        /// <param name="ticketPrice">The price of an admission ticket to the zoo.</param>
        /// <param name="waterBottlePrice">The price of a water bottle.</param>
        /// <param name="boothMoneyBalance">The initial money balance of the zoo's ticket booth.</param>
        /// <param name="attendant">The zoo's ticket booth attendant.</param>
        /// <param name="vet">The zoo's birthing room vet.</param>
        public Zoo(string name, int capacity, int restroomCapacity, decimal animalFoodPrice, decimal ticketPrice, decimal waterBottlePrice, decimal boothMoneyBalance, Employee attendant, Employee vet)
        {
            this.animalSnackMachine = new VendingMachine(animalFoodPrice, new Account());
            this.animalSnackMachine.AddMoney(42.75m);
            this.b168 = new BirthingRoom(vet);

            this.capacity = capacity;
            this.guests = new List<Guest>();
            this.ladiesRoom = new Restroom(restroomCapacity, Gender.Female);
            this.mensRoom = new Restroom(restroomCapacity, Gender.Male);
            this.name = name;
            this.ticketBooth = new MoneyCollectingBooth(attendant, ticketPrice, 3.00m, new MoneyBox());
            this.ticketBooth.AddMoney(boothMoneyBalance);
            this.informationBooth = new GivingBooth(attendant);

            this.cages = new Dictionary<Type, Cage>();

            foreach (AnimalType a in Enum.GetValues(typeof(AnimalType)))
            {
                this.cages.Add(Animal.ConvertAnimalToType(a), new Cage(400, 800));
            }

            this.animals = new List<Animal>();

            Animal brutus = new Dingo("Brutus", 3, 36.0, Gender.Male);
            Animal coco = new Dingo("Coco", 7, 38.3, Gender.Female);
            coco.AddChild(brutus);

            Animal toby = new Dingo("Toby", 4, 42.5, Gender.Male);
            Animal steve = new Dingo("Steve", 4, 41.1, Gender.Male);
            Animal maggie = new Dingo("Maggie", 7, 34.8, Gender.Female);
            maggie.AddChild(toby);
            maggie.AddChild(steve);

            Animal lucy = new Dingo("Lucy", 7, 36.5, Gender.Female);
            Animal ted = new Dingo("Ted", 7, 39.7, Gender.Male);
            Animal bella = new Dingo("Bella", 10, 40.2, Gender.Female);
            bella.AddChild(coco);
            bella.AddChild(maggie);
            bella.AddChild(lucy);
            bella.AddChild(ted);

            List<Animal> tempList = new List<Animal>();
            tempList.Add(bella);
            tempList.Add(new Dingo("Max", 12, 46.9, Gender.Male));

            this.AddAnimalsToZoo(tempList);

            // Animals for sorting
            this.AddAnimal(new Chimpanzee("Bobo", 10, 128.2, Gender.Male));
            this.AddAnimal(new Chimpanzee("Bubbles", 3, 103.8, Gender.Female));
            this.AddAnimal(new Dingo("Spot", 5, 41.3, Gender.Male));
            this.AddAnimal(new Dingo("Maggie", 6, 37.2, Gender.Female));
            this.AddAnimal(new Dingo("Toby", 0, 15.0, Gender.Male));
            this.AddAnimal(new Eagle("Ari", 12, 10.1, Gender.Female));
            this.AddAnimal(new Hummingbird("Buzz", 2, 0.02, Gender.Male));
            this.AddAnimal(new Hummingbird("Bitsy", 1, 0.03, Gender.Female));
            this.AddAnimal(new Kangaroo("Kanga", 8, 72.0, Gender.Female));
            this.AddAnimal(new Kangaroo("Roo", 0, 23.9, Gender.Male));
            this.AddAnimal(new Kangaroo("Jake", 9, 153.5, Gender.Male));
            this.AddAnimal(new Ostrich("Stretch", 26, 231.7, Gender.Male));
            this.AddAnimal(new Ostrich("Speedy", 30, 213.0, Gender.Female));
            this.AddAnimal(new Platypus("Patti", 13, 4.4, Gender.Female));
            this.AddAnimal(new Platypus("Bill", 11, 4.9, Gender.Male));
            this.AddAnimal(new Platypus("Ted", 0, 1.1, Gender.Male));
            this.AddAnimal(new Shark("Bruce", 19, 810.6, Gender.Female));
            this.AddAnimal(new Shark("Anchor", 17, 458.0, Gender.Male));
            this.AddAnimal(new Shark("Chum", 14, 377.3, Gender.Male));
            this.AddAnimal(new Squirrel("Chip", 4, 1.0, Gender.Male));
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            // Guests for sorting
            Guest darla = new Guest("Darla", 7, 10.0m, WalletColor.Brown, Gender.Male, new Account());
            Guest greg = new Guest("Greg", 35, 100.0m, WalletColor.Crimson, Gender.Male, new Account());
            this.AddGuest(greg, new Ticket(0m, 0, 0));
            this.AddGuest(darla, new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Anna", 8, 12.56m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Matthew", 42, 10.0m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Doug", 7, 11.10m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Jared", 17, 31.70m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sean", 34, 20.50m, WalletColor.Brown, Gender.Male, new Account()), new Ticket(0m, 0, 0));
            this.AddGuest(new Guest("Sally", 52, 134.20m, WalletColor.Brown, Gender.Female, new Account()), new Ticket(0m, 0, 0));

            Shark chum = new Shark("Chum", 14, 377.3, Gender.Male);
            this.AddAnimal(chum);
            Squirrel chip = new Squirrel("Chip", 4, 1.0, Gender.Male);
            this.AddAnimal(chip);
            this.AddAnimal(new Squirrel("Dale", 4, 0.9, Gender.Male));

            greg.AdoptedAnimal = chip;
            darla.AdoptedAnimal = chum;

            this.b168.OnTemperatureChange = (previousTemp, currentTemp) =>
            {
                if (this.OnBirthingRoomTemperatureChange != null)
                {
                    this.OnBirthingRoomTemperatureChange(previousTemp, currentTemp);
                }
            };
        }

        /// <summary>
        /// Gets or sets the methods attached to this delegate.
        /// </summary>
        public Action<double, double> OnBirthingRoomTemperatureChange
        {
            get
            {
                return this.onBirthingRoomTemperature;
            }

            set
            {
                this.onBirthingRoomTemperature = value;
            }
        }

        /// <summary>
        /// Gets or sets the onAddGuest Action.
        /// </summary>
        public Action<Guest> OnAddGuest
        {
            get
            {
                return this.onAddGuest;
            }

            set
            {
                this.onAddGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets the onRemoveGuest Action.
        /// </summary>
        public Action<Guest> OnRemoveGuest
        {
            get
            {
                return this.onRemoveGuest;
            }

            set
            {
                this.onRemoveGuest = value;
            }
        }

        /// <summary>
        /// Gets or sets the OnAddAnimal Action.
        /// </summary>
        public Action<Animal> OnAddAnimal
        {
            get
            {
                return this.onAddAnimal;
            }

            set
            {
                this.onAddAnimal = value;
            }
        }

        /// <summary>
        /// Gets or sets the OnRemoveAnimal Action.
        /// </summary>
        public Action<Animal> OnRemoveAnimal
        {
            get
            {
                return this.onRemoveAnimal;
            }

            set
            {
                this.onRemoveAnimal = value;
            }
        }

        /// <summary>
        /// Gets the average weight of all animals in the zoo.
        /// </summary>
        public double AverageAnimalWeight
        {
            get
            {
                return this.TotalAnimalWeight / this.animals.Count;
            }
        }

        /// <summary>
        /// Gets or sets the temperature of the zoo's birthing room.
        /// </summary>
        public double BirthingRoomTemperature
        {
            get
            {
                return this.b168.Temperature;
            }

            set
            {
                this.b168.Temperature = value;
            }
        }

        /// <summary>
        /// Gets the total weight of all animals in the zoo.
        /// </summary>
        public double TotalAnimalWeight
        {
            get
            {
                // Define accumulator variable.
                double totalWeight = 0;

                // Loop through the list of animals.
                foreach (Animal a in this.animals)
                {
                    // Add current animal's weight to the total.
                    totalWeight += a.Weight;
                }

                return totalWeight;
            }
        }

        /// <summary>
        /// Gets the animal list and returns it as an IEnumerable Type.
        /// </summary>
        public IEnumerable<Animal> Animals
        {
            get
            {
                return this.animals;
            }
        }

        /// <summary>
        /// Gets the guest list and returns it as an IEnumerable Type.
        /// </summary>
        public IEnumerable<Guest> Guests
        {
            get
            {
                return this.guests;
            }
        }

        /// <summary>
        /// Loads a specified save file.
        /// </summary>
        /// <param name="fileName">Name of the file to load.</param>
        /// <returns>Returns a loaded zoo.</returns>
        public static Zoo LoadFromFile(string fileName)
        {
            Zoo result = null;

            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Open and read a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the deserialization process is complete
            using (Stream stream = File.OpenRead(fileName))
            {
                // Deserialize (load) the file as a zoo
                result = formatter.Deserialize(stream) as Zoo;
            }

            return result;
        }

        /// <summary>
        /// Creates a new zoo.
        /// </summary>
        /// <returns>Returns the created zoo.</returns>
        public static Zoo NewZoo()
        {
            // Create an instance of the Zoo class.
            Zoo zoo = new Zoo("Como Zoo", 1000, 4, 0.75m, 15.00m, 3.00m, 3640.25m, new Employee("Sam", 42), new Employee("Flora", 98));

            return zoo;
        }

        /// <summary>
        /// Fixes the serialzation problem with loading the zoo.
        /// </summary>
        public void OnDeserialized()
        {
            this.guests.ForEach(g =>
            {
                this.OnAddGuest(g);
                g.GetVendingMachine += this.ProvideVendingMachine;
            });

            this.animals.ForEach(a =>
            {
                this.OnAddAnimal?.Invoke(a);
                a.OnPregnant = pregnantAnimal => this.b168.PregnantAnimals.Enqueue(pregnantAnimal);
            });

            if (this.OnBirthingRoomTemperatureChange != null)
            {
                this.OnBirthingRoomTemperatureChange(this.b168.Temperature, this.b168.Temperature);
            }
        }

        /// <summary>
        /// Adds an animal to the zoo.
        /// </summary>
        /// <param name="animal">The animal to add.</param>
        public void AddAnimal(Animal animal)
        {
            animal.IsActive = true;

            this.animals.Add(animal);

            this.OnAddAnimal?.Invoke(animal);

            this.cages[animal.GetType()].Add(animal);

            if (animal.IsPregnant)
            {
                this.b168.PregnantAnimals.Enqueue(animal);
            }

            animal.OnPregnant = pregnantAnimal => this.b168.PregnantAnimals.Enqueue(pregnantAnimal);
        }

        /// <summary>
        /// Adds a guest to the zoo.
        /// </summary>
        /// <param name="guest">The guest to add.</param>
        /// <param name="ticket">The guest's ticket to enter the zoo.</param>
        public void AddGuest(Guest guest, Ticket ticket)
        {
            if (ticket != null && !ticket.IsRedeemed)
            {
                ticket.Redeem();

                this.guests.Add(guest);

                this.OnAddGuest?.Invoke(guest);

                guest.GetVendingMachine += this.ProvideVendingMachine;
            }
            else
            {
                throw new NullReferenceException($"Guest {guest.Name} was not added because they did not have a ticket.");
            }
        }

        /// <summary>
        /// Aids a reproducer in giving birth.
        /// </summary>
        /// <param name="reproducer">The reproducer that is to give birth.</param>
        public void BirthAnimal()
        {
            try
            {
                IReproducer reproducer = this.b168.PregnantAnimals.Dequeue();

                // Birth animal.
                IReproducer baby = this.b168.BirthAnimal(reproducer);

                // If the baby is an animal...
                if (baby is Animal)
                {
                    // Add the baby to the zoo's list of animals.
                    this.AddAnimal(baby as Animal);
                }
            }
            catch (InvalidOperationException ex)
            {
                throw new NullReferenceException("The zoo does not have any pregnant animals.", ex);
            }
        }

        /// <summary>
        /// Sells a ticket to a guest.
        /// </summary>
        /// <param name="guest">The guest the ticket will be sold to.</param>
        /// <returns>Returns a ticket.</returns>
        public Ticket SellTicket(Guest guest)
        {
            guest.VisitInformationBooth(this.informationBooth);

            return guest.VisitTicketBoot(this.ticketBooth);
        }

        /// <summary>
        /// Removes an animal from the zoo.
        /// </summary>
        /// <param name="animal">The animal to be removed.</param>
        public void RemoveAnimal(Animal animal)
        {
            foreach (Guest g in this.guests)
            {
                if (g.AdoptedAnimal == animal)
                {
                    g.AdoptedAnimal = null;
                    this.FindCage(animal.GetType()).Remove(g);
                }
            }

            this.animals.Remove(animal);

            animal.IsActive = false;
            this.OnRemoveAnimal?.Invoke(animal);

            this.cages[animal.GetType()].Remove(animal);
        }

        /// <summary>
        /// Removes a guest from the zoo.
        /// </summary>
        /// <param name="guest">The guest to be removed.</param>
        public void RemoveGuest(Guest guest)
        {
            this.guests.Remove(guest);

            guest.IsActive = false;

            if (this.OnRemoveGuest != null)
            {
                this.OnRemoveGuest(guest);
            }

            if (guest.AdoptedAnimal != null)
            {
                Cage cage = this.FindCage(guest.AdoptedAnimal.GetType());

                cage.Remove(guest);
            }
        }

        /// <summary>
        /// Gets the animals in the zoo.
        /// </summary>
        /// <param name="type">The type of animal.</param>
        /// <returns>The enum of the animal you want to get.</returns>
        public IEnumerable<Animal> GetAnimals(Type type)
        {
            List<Animal> animalCollection = new List<Animal>();

            foreach (Animal a in this.animals)
            {
                if (a.GetType() == type)
                {
                    animalCollection.Add(a);
                }
            }

            return animalCollection;
        }

        /// <summary>
        /// Finds the cage of the correct animal based on the animal's type.
        /// </summary>
        /// <param name="animalType">The animal's type.</param>
        /// <returns>Returns the correct cage.</returns>
        public Cage FindCage(Type animalType)
        {
            Cage result = null;

            this.cages.TryGetValue(animalType, out result);

            return result;
        }

        /// <summary>
        /// Sorts the animals in the zoo.
        /// </summary>
        /// <param name="sortType">Type to sort by.</param>
        /// <param name="sortValue">Value to sort by.</param>
        /// <param name="list">List of animals.</param>
        /// <returns>Returns the result of sorting the animals.</returns>
        public SortResult SortObjects(string sortType, string sortValue, IList list)
        {
            Func<object, object, int> compareFunc = null;

            switch (sortValue)
            {
                case "animalname":
                    compareFunc = AnimalNameSortComparer;
                    break;

                case "guestname":
                    compareFunc = GuestNameSortComparer;
                    break;

                case "animalage":
                    compareFunc = AgeSortCompare;
                    break;

                case "animalweight":
                    compareFunc = WeightSortCompare;
                    break;

                case "moneybalance":
                    compareFunc = MoneyBalanceSortCompare;
                    break;
            }

            SortResult sortResult = null;

            switch (sortType)
            {
                case "bubble":
                    sortResult = list.BubbleSort(compareFunc);
                    break;

                case "selection":
                    sortResult = list.SelectionSort(compareFunc);
                    break;

                case "insertion":
                    sortResult = list.InsertionSort(compareFunc);
                    break;

                case "quick":
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    sortResult = new SortResult();

                    list.QuickSort(0, list.Count - 1, sortResult, compareFunc);
                    break;

                    stopwatch.Stop();
                    sortResult.ElapsedMilliseconds = stopwatch.ElapsedMilliseconds;

                    break;
            }

            return sortResult;
        }

        /// <summary>
        /// Saves the current version of the zoo.
        /// </summary>
        /// <param name="fileName">The file name of the saved zoo.</param>
        public void SaveToFile(string fileName)
        {
            // Create a binary formatter
            BinaryFormatter formatter = new BinaryFormatter();

            // Create a file using the passed-in file name
            // Use a using statement to automatically clean up object references
            // and close the file handle when the serialization process is complete
            using (Stream stream = File.Create(fileName))
            {
                // Serialize (save) the current instance of the zoo
                formatter.Serialize(stream, this);
            }
        }

        /// <summary>
        /// Sorts the animals in the zoo.
        /// </summary>
        /// <param name="sortType">What method of sorting.</param>
        /// <param name="sortValue">Which value to sort.</param>
        /// <returns>Sort result return.</returns>
        public SortResult SortAnimals(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.animals);
        }

        /// <summary>
        /// Sorts the guests in the zoo.
        /// </summary>
        /// <param name="sortType">What method of sorting.</param>
        /// <param name="sortValue">Which value to sort.</param>
        /// <returns>Sort result return.</returns>
        public SortResult SortGuests(string sortType, string sortValue)
        {
            return this.SortObjects(sortType, sortValue, this.guests);
        }

        /// <summary>
        /// Sorts the animal's by name.
        /// </summary>
        /// <param name="object1">First animal to compare.</param>
        /// <param name="object2">Second animal to compare.</param>
        /// <returns>string compare return.</returns>
        private static int AnimalNameSortComparer(object object1, object object2)
        {
            return string.Compare((object1 as Animal).Name, (object2 as Animal).Name);
        }

        /// <summary>
        /// Sorts the animal's by name.
        /// </summary>
        /// <param name="object1">First animal to compare.</param>
        /// <param name="object2">Second animal to compare.</param>
        /// <returns>string compare return.</returns>
        private static int GuestNameSortComparer(object object1, object object2)
        {
            return string.Compare((object1 as Guest).Name, (object2 as Guest).Name);
        }

        /// <summary>
        /// Sorts the animal's by weight.
        /// </summary>
        /// <param name="object1">First animal to compare.</param>
        /// <param name="object2">Second animal to compare.</param>
        /// <returns>weight compare return.</returns>
        private static int WeightSortCompare(object object1, object object2)
        {
            double weight1 = (object1 as Animal).Weight;
            double weight2 = (object2 as Animal).Weight;

            return weight1 == weight2 ? 0 : weight1 > weight2 ? 1 : -1;
        }

        /// <summary>
        /// Sorts the animal's by weight.
        /// </summary>
        /// <param name="object1">First animal to compare.</param>
        /// <param name="object2">Second animal to compare.</param>
        /// <returns>weight compare return.</returns>
        private static int MoneyBalanceSortCompare(object object1, object object2)
        {
            decimal money1 = (object1 as Guest).Wallet.MoneyBalance + (object1 as Guest).CheckingAccount.MoneyBalance;
            decimal money2 = (object2 as Guest).Wallet.MoneyBalance + (object2 as Guest).CheckingAccount.MoneyBalance;

            return money1 == money2 ? 0 : money1 > money2 ? 1 : -1;
        }

        /// <summary>
        /// Sorts the animal's by age.
        /// </summary>
        /// <param name="object1">First animal to compare.</param>
        /// <param name="object2">Second animal to compare.</param>
        /// <returns>age compare return.</returns>
        private static int AgeSortCompare(object object1, object object2)
        {
            double age1 = (object1 as Animal).Age;
            double age2 = (object2 as Animal).Age;

            return age1 == age2 ? 0 : age1 > age2 ? 1 : -1;
        }

        private void AddAnimalsToZoo(IEnumerable<Animal> animals)
        {
            this.animals.ForEach(a =>
            {
                this.AddAnimal(a);
                this.AddAnimalsToZoo(a.Children);
            });
        }

        private VendingMachine ProvideVendingMachine()
        {
            return this.animalSnackMachine;
        }
    }
}
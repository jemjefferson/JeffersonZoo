using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Accounts;
using Animals;
using BoothItems;
using People;
using Reproducers;
using Zoos;

namespace ZooConsole
{
    internal static class ConsoleHelper
    {
        public static string QueryHelper(Zoo zoo, string query)
        {
            string result = "Something went wrong...";

            switch (query)
            {
                case "totalanimalweight":
                    result = zoo.Animals.ToList().Sum(a => a.Weight).ToString();
                    break;
                case "averageanimalweight":
                    result = zoo.Animals.ToList().Average(a => a.Weight).ToString();
                    break;
                case "animalcount":
                    result = zoo.Animals.ToList().Count().ToString();
                    break;
                case "getheavyanimals":

                    result = "Heavy Animals:\n";
                    IEnumerable<object> animals = zoo.GetHeavyAnimals();
                    animals.ToList().ForEach(a => result += a.ToString() + "\n");

                    break;

                case "getyoungguests":

                    result = "Youngest Guests:\n";
                    IEnumerable<object> guests = zoo.GetYoungGuests();
                    guests.ToList().ForEach(g => result += g.ToString() + "\n");

                    break;

                case "getfemaledingos":

                    result = "Female Dingos:\n";
                    IEnumerable<object> dingos = zoo.GetFemaleDingoes();
                    dingos.ToList().ForEach(d => result += d.ToString() + "\n");

                    break;

                case "getguestsbyage":
                    result = "Guests By Age:\n";
                    IEnumerable<object> guestsAge = zoo.GetGuestsByAge();
                    guestsAge.ToList().ForEach(g => result += g.ToString() + "\n");
                    break;

                case "getflyinganimals":
                    result = "Flying Animals:\n";
                    IEnumerable<object> flyingAnimals = zoo.GetFlyingAnimals();
                    flyingAnimals.ToList().ForEach(a => result += a.ToString() + "\n");
                    break;

                case "getadoptedanimals":
                    result = "Guests and their Adopted Animal:\n";
                    IEnumerable<object> list = zoo.GetAdoptedAnimals();
                    list.ToList().ForEach(g => result += g.ToString() + "\n");
                    break;

                case "totalbalancebycolor":
                    result = "Total Balace of Wallets by Color:\n";
                    IEnumerable<object> totalBalance = zoo.GetTotalBalanceByWalletColor();
                    totalBalance.ToList().ForEach(w => result += w.ToString() + "\n");
                    break;

                case "averageweightbyanimaltype":
                    result = "Average Weight of Animals by Type:\n";
                    IEnumerable<object> averageWeight = zoo.GetAverageWeightByAnimalType();
                    averageWeight.ToList().ForEach(a => result += a.ToString() + "\n");
                    break;

                default:
                    break;

            }

            return result;
        }

        public static void AttachDelegates(Zoo zoo)
        {
            zoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                Console.WriteLine($"Previous temperature: {previousTemp:0.0}.");
                Console.WriteLine($"New temperature: {currentTemp:0.0}.");
            };
        }

        public static void ProcessAddCommmand(Zoo zoo, string type)
        {
            switch (type)
            {
                case "animal":
                    AddAnimal(zoo);
                    break;

                case "guest":
                    AddGuest(zoo);
                        break;

                default:
                    throw new Exception("This command only supports adding animals.");
            }
        }

        public static void ProcessShowCommand(Zoo zoo, string type, string name)
        {
            try
            {
                name = ConsoleUtil.InitialUpper(name);
                    
                switch (type)
                {
                    case "animal":
                        ShowAnimal(zoo, name);
                        break;

                    case "guest":
                        ShowGuest(zoo, name);
                        break;

                    case "cage":
                        ShowCage(zoo, name);
                        break;

                    case "children":
                        ShowChildren(zoo, name);
                        break;

                    default:
                        Console.WriteLine("please enter valid parameter, only animals and guests can be shown.");
                        break;
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Need additional parameters.");
            }
        }

        public static void SetTemperature(Zoo zoo, string temperature)
        {
            try
            {
                zoo.BirthingRoomTemperature = double.Parse(temperature);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (FormatException)
            {
                Console.WriteLine("A number must be submitted as a parameter in order to change the temperature.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("A parameter must be entered in order for the temp command to work.");
            }
        }

        public static void ProcessRemoveCommand(Zoo zoo, string type, string name)
        {
            switch (type)
            {
                case "animal":
                   name = ConsoleUtil.InitialUpper(name);
                    RemoveAnimal(zoo, name);
                    break;

                case "guest":
                    name = ConsoleUtil.InitialUpper(name);
                    RemoveGuest(zoo, name);
                    break;

                default:
                    Console.WriteLine("The Remove command only supports removing animals.");
                    break;
            }
        }

        public static void ShowHelpDetail(string command)
        {
            Dictionary<string, string> arguments;

            switch (command)
            {
                case "show":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to show (ANIMAL, GUEST, or CAGE).");
                    arguments.Add("objectName", "The name of the object to show (use an animal name for CAGE).");
                    ConsoleUtil.WriteHelpDetail(command, "Show details of an object.", arguments);
                    break;
                case "remove":
                    arguments = new Dictionary<string, string>();
                    arguments.Add("objectType", "The type of object to remove (ANIMAL or GUEST).");
                    arguments.Add("objectName", "The name of the object to remove.");
                    ConsoleUtil.WriteHelpDetail(command, "Removes an object from the zoo.", arguments);
                    break;
                case "temp":
                    ConsoleUtil.WriteHelpDetail(command, "Sets the temperature of the birthing room.", "temperature", "Temperature to set the temperature to (in farenheight).");
                    break;
                case "add":
                    ConsoleUtil.WriteHelpDetail(command, "Adds an object to the zoo.", "objectType", "The type of object to add (ANIMAL or GUEST).");
                    break;
                case "save":
                    ConsoleUtil.WriteHelpDetail(command, "Saves the zoo.", "fileName", "The name of the save file.");
                    break;
                case "load":
                    ConsoleUtil.WriteHelpDetail(command, "Loads a zoo.", "fileName", "The name of the save file to load.");
                    break;
                case "restart":
                    ConsoleUtil.WriteHelpDetail(command, "Creates a new zoo.");
                    break;
                case "exit":
                    ConsoleUtil.WriteHelpDetail(command, "Exits the application.");
                    break;
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("Zoo Help Index:");
            ConsoleUtil.WriteHelpDetail("help", "Show help detail.", "[command]", "The (optional) command for which to show help details.");
            Console.WriteLine("Known commands:");
            Console.WriteLine("RESTART: Creates a new zoo.");
            Console.WriteLine("EXIT: Exits the application.");
            Console.WriteLine("TEMP: Sets the temperature of the zoo's birthing room.");
            Console.WriteLine("SHOW: Shows the properties of an animal, guest, or cage.");
            Console.WriteLine("ADD: Adds an animal or guest to the zoo.");
            Console.WriteLine("REMOVE: Removes a guest or animal from the zoo.");
            Console.WriteLine("SAVE: Saves the current zoo and all of it's assets.");
            Console.WriteLine("LOAD: Loads a saved zoo.");
        }

        public static void SaveFile(Zoo zoo, string fileName)
        {
            try
            {
                zoo.SaveToFile(fileName);
                Console.WriteLine("Your zoo has been successfully saved.");
            }
            catch
            {
                Console.WriteLine("Error: save was unsuccessful.");
            }
        }

        public static Zoo LoadFile(string fileName)
        {
            Zoo zoo = null;

            try
            {
                zoo = Zoo.LoadFromFile(fileName);
                Console.WriteLine("The load was successful");
            }
            catch
            {
                Console.WriteLine("The load was unsuccessful.");
            }

            return zoo;
        }

        private static void AddAnimal(Zoo zoo)
        {
            bool success = false;

            Animal animal = AnimalFactory.CreateAnimal(ConsoleUtil.ReadAnimalType(), "Name", 1, 0.0, Gender.Male);

            animal.Name = ConsoleUtil.ReadAlphabeticValue("Name");
            animal.Name = ConsoleUtil.InitialUpper(animal.Name);

            animal.Gender = ConsoleUtil.ReadGender();

            while (success == false)
            {
                try
                {
                    animal.Age = ConsoleUtil.ReadIntValue("Age");
                    success = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            success = false;

            while (success == false)
            {
                try
                {
                    animal.Weight = ConsoleUtil.ReadDoubleValue("Weight");
                    success = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            zoo.AddAnimal(animal);
            ShowAnimal(zoo, animal.Name);

        }

        private static void AddGuest(Zoo zoo)
        {
            bool success = false;

            Guest guest = new Guest("Jim", 37, 231.67m, WalletColor.Black, Gender.Male, new Account());

            guest.Name = ConsoleUtil.ReadAlphabeticValue("Name");
            guest.Name = ConsoleUtil.InitialUpper(guest.Name);

            guest.Gender = ConsoleUtil.ReadGender();

            while (success == false)
            {
                try
                {
                    guest.Age = ConsoleUtil.ReadIntValue("Age");

                    success = true;
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            guest.Wallet.MoneyBalance = Convert.ToDecimal(ConsoleUtil.ReadDoubleValue("Wallet Balance"));
            guest.Wallet.WalletColor = ConsoleUtil.ReadWalletColor();
            guest.CheckingAccount.MoneyBalance = Convert.ToDecimal(ConsoleUtil.ReadDoubleValue("Checking Account Balance"));

            Ticket ticket = zoo.SellTicket(guest);

            zoo.AddGuest(guest, ticket);
            ShowGuest(zoo, guest.Name);

        }

        private static void ShowAnimal(Zoo zoo, string name)
        {
            try
            {
 
                Animal animal = zoo.FindAnimal(a => a.Name == name);

                if (animal != null)
                {
                    Console.WriteLine($"The following animal was found: {animal}");
                }
                else if (animal == null)
                {
                    Console.WriteLine("That animal was not found.");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Enter a third parameter of the name of an animal.");
            }
        }

        private static void ShowGuest(Zoo zoo, string name)
        {
            try
            {
                Guest guest = zoo.FindGuest(g => g.Name == name);
                if (guest != null)
                {
                    Console.WriteLine($"The following guest was found: {guest}");
                }
                else if (guest == null)
                {
                    Console.WriteLine("That guest was not found.");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Enter a third parameter of the name of a guest.");
            }
        }

        private static void RemoveAnimal(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);

            if (animal != null)
            {
                zoo.RemoveAnimal(animal);
                Console.WriteLine($"The animal [{name}] was removed.");
            }
            else if (animal == null)
            {
                Console.WriteLine($"The animal was not found.");
            }
        }

        private static void RemoveGuest(Zoo zoo, string name)
        {
            Guest guest = zoo.FindGuest(g => g.Name == name);

            if (guest != null)
            {
                zoo.RemoveGuest(guest);
                Console.WriteLine($"The guest {guest} was removed.");
            }
            else if (guest == null)
            {
                string value = $"The guest was not found.";
                Console.WriteLine(value);
            }
        }

        private static void ShowCage(Zoo zoo, string animalName)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == animalName);

            if (animal != null)
            {
                Cage cage = zoo.FindCage(animal.GetType());

                if (cage != null)
                {
                    Console.WriteLine(cage.ToString());
                }
                else
                {
                    Console.WriteLine("This animal's cage does not exist.");
                }
            }
            else
            {
                Console.WriteLine("The cage can not be found because the animal does not exist.");
            }
        }

        private static void ShowChildren(Zoo zoo, string name)
        {
            Animal animal = zoo.FindAnimal(a => a.Name == name);
            WalkTree(animal, string.Empty);
        }

        private static void WalkTree(Animal animal, string prefix)
        {
            Console.WriteLine($"{prefix}{animal.Name}");

            foreach (Animal a in animal.Children)
            {
                WalkTree(a, prefix + "  ");
            }
        }
    }
}

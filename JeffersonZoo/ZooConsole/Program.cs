using System;
using BirthingRooms;
using People;
using ZooScenario;
using Zoos;
using Animals;
using System.Collections.Generic;
using System.Linq;

namespace ZooConsole
{
    public class Program
    {
        /// <summary>
        /// A zoo.
        /// </summary>
        private static Zoo zoo;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Jefferson Zoo!\n");
            Console.WriteLine("Type help to view a list of commands.");

            Console.Title = "Jefferson Zoo";

            zoo = Zoo.NewZoo();

            ConsoleHelper.AttachDelegates(zoo);

            bool exit = false;

            try
            {
                while (!exit)
                {
                    Console.Write("] ");

                    string command = Console.ReadLine();

                    string[] commandWords = command.Split();

                    command = command.ToLower().Trim();

                    switch (commandWords[0])
                    {
                        case "exit":
                            exit = true;
                            break;

                        case "restart":
                            zoo = Zoo.NewZoo();
                            ConsoleHelper.AttachDelegates(zoo);
                            zoo.BirthingRoomTemperature = 77;
                            Console.WriteLine("A new Como Zoo has been created.");
                            break;

                        case "help":
                            if (commandWords.Length == 1)
                            {
                                ConsoleHelper.ShowHelp();
                            }
                            if (commandWords.Length == 2)
                            {
                                ConsoleHelper.ShowHelpDetail(commandWords[1]);
                            }
                            if (commandWords.Length > 2)
                            {
                                Console.WriteLine("Too many parameters were entered.");
                            }
                            break;

                        case "temp":
                            try
                            {
                                ConsoleHelper.SetTemperature(zoo, commandWords[1]);
                            }
                            catch (IndexOutOfRangeException)
                            {
                                Console.WriteLine("Please enter a parameter for temperature");
                            }
                            break;

                        case "show":
                            try
                            {
                                ConsoleHelper.ProcessShowCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest] [name].");
                            }
                            break;

                        case "add":
                            try
                            {
                                ConsoleHelper.ProcessAddCommmand(zoo, commandWords[1]);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter the parameters [animal or guest].");
                            }
                            break;

                        case "remove":
                            try
                            {
                                ConsoleHelper.ProcessRemoveCommand(zoo, commandWords[1], commandWords[2]);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter a guest or animal you would like to remove.");
                            }
                            break;

                        case "sort":
                            try
                            {
                                SortResult sortResult = null;

                                // Changes for 6.4A.
                                string sortType = commandWords[2].ToLower();
                                string sortValue = commandWords[3].ToLower();
                                //

                                if (commandWords[1] == "animals")
                                {
                                    sortResult = zoo.SortAnimals(sortType, sortValue);

                                    foreach (Animal a in sortResult.Objects)
                                    {
                                        Console.WriteLine(a.ToString());
                                    }
                                }

                                if (commandWords[1] == "guests")
                                {
                                    sortResult = zoo.SortGuests(sortType, sortValue);

                                    foreach (Guest g in sortResult.Objects)
                                    {
                                        Console.WriteLine(g.ToString());
                                    }
                                }

                                Console.WriteLine("SORT TYPE: " + commandWords[2].ToUpper());
                                Console.WriteLine("SORT BY: " + commandWords[1].ToUpper());
                                Console.WriteLine("SORT VALUE: " + commandWords[3].ToUpper());
                                Console.WriteLine("SWAP COUNT: " + sortResult.SwapCount);
                                Console.WriteLine("COMPARE COUNT: " + sortResult.CompareCount);
                                Console.WriteLine("TIME: " + sortResult.ElapsedMilliseconds);
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Sort command must be entered as: sort [sort by -- animals or guests] [sort type] [sort value -- animal[Value] or guest[Value].");
                            }
                            break;

                        case "search":
                            if (commandWords[1] == "binary")
                            {
                                int loopCounter = 0;
                                string animalName = ConsoleUtil.InitialUpper(commandWords[2]);
                                List<Animal> animals = zoo.Animals.ToList();

                                int minPosition = 0;
                                int maxPosition = animals.Count - 1;

                                while (minPosition <= maxPosition)
                                {
                                    int middlePosition = (minPosition + maxPosition) / 2;
                                    loopCounter += 1;

                                    int compare = string.Compare(animalName, animals[middlePosition].Name);

                                    if (compare > 0)
                                    {
                                        minPosition = middlePosition + 1;
                                    }
                                    else if (compare < 0)
                                    {
                                        maxPosition = middlePosition - 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{animalName} was found. {loopCounter} loops complete.");
                                        break;
                                    }
                                }
                            }

                            if (commandWords[1] == "linear")
                            {
                                int loopCounter = 0;
                                string animalName = ConsoleUtil.InitialUpper(commandWords[2]);

                                foreach (Animal a in zoo.Animals)
                                {
                                    loopCounter += 1;
                                    
                                    if (a.Name == animalName)
                                    {
                                        Console.WriteLine($"{animalName} found. {loopCounter} loops complete.");
                                        break;
                                    }
                                }
                            }

                            if (commandWords[1] == "guests")
                            {
                            }

                            break;

                        case "save":
                            try
                            {
                                ConsoleHelper.SaveFile(zoo, commandWords[1]);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter the name of the save file.");
                            }
                            break;

                        case "load":
                            try
                            {
                               zoo = ConsoleHelper.LoadFile(commandWords[1]);
                                ConsoleHelper.AttachDelegates(zoo);
                            }
                            catch
                            {
                                Console.WriteLine("Please enter the name of the save file to load.");
                            }
                            break;

                        case "query":
                            string query = ConsoleHelper.QueryHelper(zoo, commandWords[1]);
                            Console.WriteLine(query);
                            break;

                        default:
                            Console.WriteLine($"Command '{command}' does not exist.");
                            break;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

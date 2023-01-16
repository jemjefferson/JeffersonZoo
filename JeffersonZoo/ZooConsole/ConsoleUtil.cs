using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Animals;
using People;
using Reproducers;
using Utilities;

namespace ZooConsole
{
    internal static class ConsoleUtil
    {
        /// <summary>
        /// This method capitalizes the first letter of a string.
        /// </summary>
        /// <param name="value">The string value to be capitalized.</param>
        /// <returns>Returns a string value.</returns>
        public static string InitialUpper(string value)
        {
            if (value != null && value.Length > 0)
            {
                value = char.ToUpper(value[0]) + value.Substring(1);
            }

            return value;
        }

        public static string ReadAlphabeticValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                result = ConsoleUtil.ReadStringValue(prompt);

                if (Regex.IsMatch(result, @"^[a-zA-Z ]+$"))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must contain only letters or spaces.");
                }
            }

            return result;
        }

        public static double ReadDoubleValue(string prompt)
        {
            double result = 0.0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (double.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be either a whole number or a decimal number.");
                }
            }

            return result;
        }

        public static Gender ReadGender()
        {
            Gender result = Gender.Female;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Gender");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse<Gender>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid gender. Possible gender types: " + GetTypes(typeof(Gender)));
                }
            }

            return result;
        }

        public static WalletColor ReadWalletColor()
        {
            WalletColor result = WalletColor.Black;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Wallet Color");
                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse<WalletColor>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid color. Possible color types: " + GetTypes(typeof(WalletColor)));
                }
            }

            return result;
        }
        
        public static int ReadIntValue(string prompt)
        {
            int result = 0;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadStringValue(prompt);

                if (int.TryParse(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must be a whole number.");
                }
            }

            return result;
        }

        public static string ReadStringValue(string prompt)
        {
            string result = null;

            bool found = false;

            while (!found)
            {
                Console.Write(prompt + "] ");

                string stringValue = Console.ReadLine().ToLower().Trim();

                if (stringValue != string.Empty)
                {
                    result = stringValue;
                    found = true;
                }
                else
                {
                    Console.WriteLine(prompt + " must have a value.");
                }
            }

            return result;
        }

        public static string GetTypes(Type type)
        {
            StringBuilder typeList = new StringBuilder();

            foreach (string at in Enum.GetNames(type))
            {
                typeList.Append(at + "|");
            }

            return "|" + typeList.ToString();
        }

        public static AnimalType ReadAnimalType()
        {
            AnimalType result = AnimalType.Chimpanzee;

            string stringValue = result.ToString();

            bool found = false;

            while (!found)
            {
                stringValue = ConsoleUtil.ReadAlphabeticValue("Animal Type");

                stringValue = ConsoleUtil.InitialUpper(stringValue);

                if (Enum.TryParse<AnimalType>(stringValue, out result))
                {
                    found = true;
                }
                else
                {
                    Console.WriteLine("Invalid animal type. Possible animal types: " + GetTypes(typeof(AnimalType)));
                }
            }

            return result;
        }

        public static void WriteHelpDetail(string command, string overview, Dictionary<string, string> arguments)
        {
                string upperCommand = command.ToUpper();

                Console.WriteLine($"Command name: {upperCommand}");
                Console.WriteLine($"Overview: {overview}");

            if (arguments != null)
            {
                Console.WriteLine($"Usage: {upperCommand} {arguments.Keys.Flatten(" ")}");

                Console.WriteLine("Parameters:");

                arguments.ToList().ForEach(kvp => Console.WriteLine($"{kvp.Key}: {kvp.Value}"));
            }
        }

        public static void WriteHelpDetail(string command, string overview, string argument, string argumentUsage)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            dictionary.Add(argument, argumentUsage);

            WriteHelpDetail(command, overview, dictionary);
        }

        public static void WriteHelpDetail(string command, string overview)
        {
            WriteHelpDetail(command, overview, null);
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Accounts;
using Animals;
using BirthingRooms;
using BoothItems;
using Microsoft.Win32;
using People;
using Reproducers;
using VendingMachines;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Contains interaction logic for MainWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class MainWindow : Window
    {
        private const string AutoSaveFileName = "Autosave.zoo";

        /// <summary>
        /// Minnesota's Como Zoo.
        /// </summary>
        private Zoo comoZoo;

        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();

            #if DEBUG
            this.Title += " [DEBUG]";
            #endif
        }

        private void admitGuestButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guest ethel = new Guest("Ethel", 42, 30.00m, WalletColor.Salmon, Reproducers.Gender.Male, new Wallet(WalletColor.Salmon));

                Ticket ticket = this.comoZoo.SellTicket(ethel);

                this.comoZoo.AddGuest(ethel, ticket);
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException.GetType().ToString() + ": " + ex.InnerException.Message);
            }
        }

        private void feedAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (guest != null & animal != null)
            {
                guest.FeedAnimal(animal);
            }
            else
            {
                MessageBox.Show("Please choose both a guest and an animal to feed an animal.");
            }

            guest = this.guestListBox.SelectedItem as Guest;
            animal = this.animalListBox.SelectedItem as Animal;
        }

        private void increaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature += 1;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void decreaseTemperatureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthingRoomTemperature -= 1;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            bool result = this.LoadZoo(AutoSaveFileName);

            if (result == false)
            {
                this.comoZoo = Zoo.NewZoo();
                this.AttachDelegates();
            }

            this.animalTypeComboBox.ItemsSource = Enum.GetValues(typeof(AnimalType));

            this.changeMoveBehaviorComboBox.ItemsSource = Enum.GetValues(typeof(MoveBehaviorType));
        }

        private void addAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimalType animalType = (AnimalType)this.animalTypeComboBox.SelectedItem;

                Animal animal = AnimalFactory.CreateAnimal(animalType, "Name", 10, 50, Gender.Male);

                AnimalWindow animalWindow = new AnimalWindow(animal);
                animalWindow.ShowDialog();

                if (animalWindow.DialogResult == true)
                {
                    this.comoZoo.AddAnimal(animal);
                }
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("An animal must be selected before adding an animal to the zoo.");
            }
        }

        private void addGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = new Guest("Name", 1, 0.0m, WalletColor.Black, Gender.Male, new Account());

            GuestWindow guestWindow = new GuestWindow(guest);
            guestWindow.ShowDialog();

            try
            {
                if (guestWindow.DialogResult == true)
                {
                    Ticket ticket = this.comoZoo.SellTicket(guest);
                    this.comoZoo.AddGuest(guest, ticket);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void removeAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove animal: {0}?", animal.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveAnimal(animal);
                }
            }
            else if (animal == null)
            {
                MessageBox.Show("Select an animal to remove.");
            }
        }

        private void removeGuestButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                if (MessageBox.Show(string.Format("Are you sure you want to remove guest: {0}?", guest.Name), "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.comoZoo.RemoveGuest(guest);
                }
            }
            else if (guest == null)
            {
                MessageBox.Show("Select a guest to remove.");
            }
        }

        private void animalListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                AnimalWindow animalWindow = new AnimalWindow(animal);
                animalWindow.ShowDialog();

                if (animalWindow.DialogResult == true)
                {
                    if (animal.IsPregnant == true)
                    {
                        this.comoZoo.RemoveAnimal(animal);
                        this.comoZoo.AddAnimal(animal);
                    }
                }
            }
        }

        private void guestListBox_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;

            if (guest != null)
            {
                GuestWindow guestWindow = new GuestWindow(guest);
                guestWindow.ShowDialog();
            }
        }

        private void showCageButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (animal != null)
            {
                CageWindow cageWindow = new CageWindow(this.comoZoo.FindCage(animal.GetType()));
                cageWindow.Show();
            }
            else
            {
                MessageBox.Show("Please select an animal to show.");
            }
        }

        private void adoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;
            Guest guest = this.comoZoo.FindGuest(g => g.AdoptedAnimal == null);

            if (animal != null)
            {
                Cage cage = this.comoZoo.FindCage(animal.GetType());

                if (guest != null)
                {
                    guest.AdoptedAnimal = animal;
                    cage.Add(guest);
                }
                else
                {
                    MessageBox.Show("There are no guests available to adopt the animal.");
                }
            }
            else if (animal == null)
            {
                MessageBox.Show("Please select an animal to adopt.");
            }
        }

        private void unadoptAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            Guest guest = this.guestListBox.SelectedItem as Guest;
            Animal animal = this.animalListBox.SelectedItem as Animal;

            if (guest == null && animal == null)
            {
                MessageBox.Show("Please select a guest and an animal.");
            }
            else if (guest == null)
            {
                MessageBox.Show("Please select a guest to unadopt the animal.");
            }
            else
            {
                if (animal == guest.AdoptedAnimal)
                {
                    guest.AdoptedAnimal = null;
                    Cage cage = this.comoZoo.FindCage(animal.GetType());
                    cage.Remove(guest);
                }
                else
                {
                    MessageBox.Show($"The animal named {animal.Name} is not the guest's adopted animal.");
                }
            }
        }

        private void changMoveBehaviorButton_Click(object sender, RoutedEventArgs e)
        {
            Animal animal = this.animalListBox.SelectedItem as Animal;

            object selectedBehavior = this.changeMoveBehaviorComboBox.SelectedItem;

            if (animal != null && selectedBehavior != null)
            {
                animal.MoveBehavior = MoveBehaviorFactory.CreateMoveBehavior((MoveBehaviorType)selectedBehavior);
            }
            else
            {
                MessageBox.Show("Please select both an animal from the list of animals and a movement type.");
            }
        }

        private void birthAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.comoZoo.BirthAnimal();
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (dialog.ShowDialog() == true)
            {
                this.SaveZoo(dialog.FileName);
            }
        }

        private void SaveZoo(string fileName)
        {
            this.comoZoo.SaveToFile(fileName);
            this.SetWindowTitle(fileName);
        }

        private void SetWindowTitle(string fileName)
        {
            // Set the title of the window using the current file name
            this.Title = $"Object-Oriented Programming 2: Zoo [{Path.GetFileName(fileName)}]";
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Zoo save-game files (*.zoo)|*.zoo";

            if (dialog.ShowDialog() == true)
            {
                this.LoadZoo(dialog.FileName);
            }
        }

        private bool LoadZoo(string fileName)
        {
            bool result = true;

            try
            {
                this.comoZoo = Zoo.LoadFromFile(fileName);
                this.AttachDelegates();
                this.SetWindowTitle(fileName);
            }
            catch
            {
                MessageBox.Show("The zoo could not be loaded.");
                result = false;
            }

            return result;
        }

        private void ClearWindow()
        {
            this.animalListBox.ItemsSource = null;
            this.guestListBox.Items.Clear();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start over?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                this.ClearWindow();

                this.comoZoo = Zoo.NewZoo();

                this.AttachDelegates();

                this.Title = "Object-Oriented Programming 2: Zoo";
            }
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.SaveZoo(AutoSaveFileName);
        }

        private void AttachDelegates()
        {
            this.comoZoo.OnBirthingRoomTemperatureChange = (previousTemp, currentTemp) =>
            {
                this.temperatureBorder.Height = currentTemp * 2;
                this.temperatureLabel.Content = string.Format("{0: 0.0} °F", currentTemp);

                double colorLevel = ((currentTemp - BirthingRoom.MinTemperature) * 255) / (BirthingRoom.MaxTemperature - BirthingRoom.MinTemperature);

                this.temperatureBorder.Background = new SolidColorBrush(Color.FromRgb(
                    Convert.ToByte(colorLevel),
                    Convert.ToByte(255 - colorLevel),
                    Convert.ToByte(255 - colorLevel)));
            };

            this.comoZoo.OnAddGuest = g =>
            {
                this.guestListBox.Items.Add(g);

                g.OnTextChange += this.UpdateGuestDisplay;
            };

            this.comoZoo.OnRemoveGuest = g =>
            {
                this.guestListBox.Items.Remove(g);

                g.OnTextChange -= this.UpdateGuestDisplay;
            };

            this.comoZoo.OnAddAnimal = a =>
            {
                this.animalListBox.Items.Add(a);

                a.OnTextChange += this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnRemoveAnimal = a =>
            {
                this.animalListBox.Items.Remove(a);

                a.OnTextChange -= this.UpdateAnimalDisplay;
            };

            this.comoZoo.OnDeserialized();
        }

        private void UpdateGuestDisplay(Guest guest)
        {
            int index = this.guestListBox.Items.IndexOf(guest);

            if (index >= 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    this.guestListBox.Items.RemoveAt(index);

                    this.guestListBox.Items.Insert(index, guest);

                    this.guestListBox.SelectedItem = this.guestListBox.Items[index];
                });
            }
        }

        private void UpdateAnimalDisplay(Animal animal)
        {
            int index = this.animalListBox.Items.IndexOf(animal);

            if (index >= 0)
            {
                this.Dispatcher.Invoke(() =>
                {
                    // disconnect the guest
                    this.animalListBox.Items.RemoveAt(index);

                    // create new guest item in the same spot
                    this.animalListBox.Items.Insert(index, animal);

                    // re-select the guest
                    this.animalListBox.SelectedItem = this.animalListBox.Items[index];
                });
            }
        }
    }
}
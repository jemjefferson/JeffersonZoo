using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for AnimalWindow.xaml.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Event handlers may begin with lower-case letters.")]
    public partial class AnimalWindow : Window
    {
        /// <summary>
        /// The animal to be created.
        /// </summary>
        private Animal animal;

        /// <summary>
        /// Instantiates a new instance of the AnimalWindow class.
        /// </summary>
        /// <param name="animal">The animal to be created.</param>
        public AnimalWindow(Animal animal)
        {
            this.animal = animal;
            this.InitializeComponent();
        }

        private void animalWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.animal.Name;
            this.ageTextBox.Text = this.animal.Age.ToString();
            this.weightTextBox.Text = this.animal.Weight.ToString();

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.animal.Gender;

            this.pregnancyStatusLabel.Content = this.animal.IsPregnant ? "Yes" : "No";
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void nameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Name = this.nameTextBox.Text;
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void weightTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.animal.Weight = double.Parse(this.weightTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void makePregnantButton_Click(object sender, RoutedEventArgs e)
        {
            this.animal.MakePregnant();
            this.makePregnantButton.IsEnabled = false;
            this.pregnancyStatusLabel.Content = "Yes";
        }

        private void genderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.animal.Gender = (Gender)this.genderComboBox.SelectedItem;

            this.makePregnantButton.IsEnabled = (this.animal.Gender == Gender.Female) ? this.makePregnantButton.IsEnabled = true : this.makePregnantButton.IsEnabled = false;
        }
    }
}

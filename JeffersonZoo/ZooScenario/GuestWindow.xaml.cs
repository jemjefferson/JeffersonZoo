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
using People;
using Reproducers;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml.
    /// </summary>
    public partial class GuestWindow : Window
    {
        private Guest guest;

        /// <summary>
        /// Instantiates an instance of the guest window.
        /// </summary>
        /// <param name="guest">The guest being created.</param>
        public GuestWindow(Guest guest)
        {
            this.guest = guest;

            this.InitializeComponent();
        }

        private void GuestWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.nameTextBox.Text = this.guest.Name;
            this.ageTextBox.Text = this.guest.Age.ToString();

            this.genderComboBox.ItemsSource = Enum.GetValues(typeof(Gender));
            this.genderComboBox.SelectedItem = this.guest.Gender;

            this.walletColorComboBox.ItemsSource = Enum.GetValues(typeof(WalletColor));
            this.walletColorComboBox.SelectedItem = this.guest.Wallet.WalletColor;

            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");

            this.moneyAccountComboBox.Items.Add(1);
            this.moneyAccountComboBox.Items.Add(5);
            this.moneyAccountComboBox.Items.Add(10);
            this.moneyAccountComboBox.Items.Add(20);

            this.moneyAccountComboBox.SelectedItem = this.moneyAccountComboBox.Items[0];

            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");

            this.accountComboBox.Items.Add(1);
            this.accountComboBox.Items.Add(5);
            this.accountComboBox.Items.Add(10);
            this.accountComboBox.Items.Add(20);
            this.accountComboBox.Items.Add(50);
            this.accountComboBox.Items.Add(100);

            this.accountComboBox.SelectedItem = this.accountComboBox.Items[0];
        }

        private void GuestOKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NameTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Name = this.nameTextBox.Text;
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AgeTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                this.guest.Age = int.Parse(this.ageTextBox.Text);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Gender = (Gender)this.genderComboBox.SelectedItem;
        }

        private void WalletColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.guest.Wallet.WalletColor = (WalletColor)this.walletColorComboBox.SelectedItem;
        }

        private void AddMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.Wallet.AddMoney(Convert.ToDecimal(this.moneyAccountComboBox.Text));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        private void SubtractMoneyButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.Wallet.RemoveMoney(Convert.ToDecimal(this.moneyAccountComboBox.Text));
            this.moneyBalanceLabel.Content = this.guest.Wallet.MoneyBalance.ToString("C");
        }

        private void AddAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.AddMoney(Convert.ToDecimal(this.accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }

        private void SubtractAccountButton_Click(object sender, RoutedEventArgs e)
        {
            this.guest.CheckingAccount.RemoveMoney(Convert.ToDecimal(this.accountComboBox.Text));
            this.accountBalanceLabel.Content = this.guest.CheckingAccount.MoneyBalance.ToString("C");
        }
    }
}

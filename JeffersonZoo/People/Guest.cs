using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using Accounts;
using Animals;
using BoothItems;
using CagedItems;
using Foods;
using MoneyCollectors;
using Reproducers;
using Utilities;
using VendingMachines;

namespace People
{
    /// <summary>
    /// The class which is used to represent a guest.
    /// </summary>
    [Serializable]
    public class Guest : IEater, ICageable
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the guest.
        /// </summary>
        private int age;

        private Animal adoptedAnimal;

        /// <summary>
        /// The guest's bag of items.
        /// </summary>
        private List<Item> bag;

        /// <summary>
        /// The name of the guest.
        /// </summary>
        private string name;

        /// <summary>
        /// The guest's wallet.
        /// </summary>
        private Wallet wallet;

        /// <summary>
        /// The guest's gender.
        /// </summary>
        private Gender gender;

        /// <summary>
        /// The guest's checking account.
        /// </summary>
        private IMoneyCollector checkingAccount;

        [NonSerialized]
        private Action<Guest> onTextChange;

        [NonSerialized]
        private Timer feedTimer;

        private bool isActive;

        /// <summary>
        /// Initializes a new instance of the Guest class.
        /// </summary>
        /// <param name="name">The name of the guest.</param>
        /// <param name="age">The age of the guest.</param>
        /// <param name="moneyBalance">The initial amount of money to put into the guest's wallet.</param>
        /// <param name="walletColor">The color of the guest's wallet.</param>
        /// <param name="gender">The guest's gender.</param>
        /// <param name="checkingAccount">The guest's money account.</param>
        public Guest(string name, int age, decimal moneyBalance, WalletColor walletColor, Gender gender, IMoneyCollector checkingAccount)
        {
            this.age = age;
            this.name = name;
            this.wallet = new Wallet(walletColor);
            this.wallet.AddMoney(moneyBalance);
            this.gender = gender;
            this.bag = new List<Item>();
            this.checkingAccount = checkingAccount;

            this.checkingAccount.OnBalanceChange += this.HandleBalanceChange;
            this.wallet.OnBalanceChange += this.HandleBalanceChange;

            this.XPosition = random.Next(0, 201);
            this.YPosition = 400;

            this.CreateTimers();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the animal is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.isActive || this.AdoptedAnimal != null;
            }

            set
            {
                this.isActive = value;
            }
        }

        /// <summary>
        /// Gets or sets the OnTextChange Action.
        /// </summary>
        public Action<Guest> OnTextChange
        {
            get
            {
                return this.onTextChange;
            }

            set
            {
                this.onTextChange = value;
            }
        }

        /// <summary>
        /// Gets or sets the adopted animal.
        /// </summary>
        public Animal AdoptedAnimal
        {
            get
            {
                return this.adoptedAnimal;
            }

            set
            {
                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger = null;
                }

                this.adoptedAnimal = value;

                if (this.adoptedAnimal != null)
                {
                    this.adoptedAnimal.OnHunger = this.HandleAnimalHungry;
                }

                if (this.OnTextChange != null)
                {
                    this.OnTextChange(this);
                }
            }
        }

        /// <summary>
        /// Gets or sets the age of the guest.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value >= 0 && value <= 120)
                {
                    this.age = value;

                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Age must be between 0 and 120");
                }
            }
        }

        /// <summary>
        /// Gets the guest's checking account.
        /// </summary>
        public IMoneyCollector CheckingAccount
        {
            get
            {
                return this.checkingAccount;
            }
        }

        /// <summary>
        /// Gets or sets the gender of the guest.
        /// </summary>
        public Gender Gender
        {
            get
            {
                return this.gender;
            }

            set
            {
                this.gender = value;
            }
        }

        /// <summary>
        /// Gets or sets the name of the guest.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (Regex.IsMatch(value, @"^[a-zA-Z ]+$"))
                {
                    this.name = value;

                    if (this.OnTextChange != null)
                    {
                        this.OnTextChange(this);
                    }
                }
                else
                {
                    throw new FormatException("Name must only include letters and spaces.");
                }
            }
        }

        /// <summary>
        /// Gets the guest's wallet.
        /// </summary>
        public Wallet Wallet
        {
            get
            {
                return this.wallet;
            }
        }

        /// <summary>
        /// Gets or sets the weight of the guest.
        /// </summary>
        public double Weight
        {
            get
            {
                // Confidential.
                return 0.0;
            }

            set
            {
                this.Weight = value;
            }
        }

        /// <summary>
        /// Gets the weight gain after eating.
        /// </summary>
        public double WeightGainPercentage
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets the display size.
        /// </summary>
        public double DisplaySize
        {
            get
            {
                return 0.6;
            }
        }

        /// <summary>
        /// Gets the resource key.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                return "Guest";
            }
        }

        /// <summary>
        /// Gets the pixel coordinates (where in the cage) that the animal we be drawn at.
        /// </summary>
        public int XPosition { get; private set; }

        /// <summary>
        /// Gets the pixel coordinates (where in the cage) that the animal we be drawn at.
        /// </summary>
        public int YPosition { get; private set; }

        /// <summary>
        /// Gets the way in which the animal will be facing (left or right/up or down).
        /// </summary>
        public HorizontalDirection XDirection { get; private set; }

        /// <summary>
        /// Gets the way in which the animal will be facing (left or right/up or down).
        /// </summary>
        public VerticalDirection YDirection { get; private set; }

        /// <summary>
        /// Gets the hungerstate of the guest.
        /// </summary>
        public HungerState HungerState { get; }

        /// <summary>
        /// Gets or sets a vending machine for the guest to use to feed an animal.
        /// </summary>
        public Func<VendingMachine> GetVendingMachine { get; set; }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// Handles the adopted animal's hunger.
        /// </summary>
        public void HandleAnimalHungry()
        {
            this.feedTimer.Start();
        }

        /// <summary>
        /// Feeds the animal and stops the timer.
        /// </summary>
        /// <param name="sender">The feed timer.</param>
        /// <param name="e">The event when the timer is hit.</param>
        public void HandleReadyToFeed(object sender, ElapsedEventArgs e)
        {
            this.FeedAnimal(this.AdoptedAnimal);
            this.feedTimer.Stop();
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public void Eat(Food food)
        {
            // Eat the food.
        }

        /// <summary>
        /// Feeds the specified eater.
        /// </summary>
        /// <param name="eater">The eater to be fed.</param>
        public void FeedAnimal(IEater eater)
        {
            VendingMachine animalSnackMachine = this.GetVendingMachine();

            decimal price = animalSnackMachine.DetermineFoodPrice(eater.Weight);

            if (this.wallet.MoneyBalance < price)
            {
                this.WithdrawMoney(price * 5);
            }

            decimal payment = this.wallet.RemoveMoney(price);

            Food food = animalSnackMachine.BuyFood(payment);

            eater.Eat(food);
        }

        /// <summary>
        /// Generates a string representation of the guest.
        /// </summary>
        /// <returns>A string representation of the guest.</returns>
        public override string ToString()
        {
            string guestName = this.Name + ": " + this.Age + $" [${this.Wallet.MoneyBalance} / ${this.CheckingAccount.MoneyBalance}]";
            guestName = (this.AdoptedAnimal != null) ? guestName + " " + this.AdoptedAnimal.Name : guestName + string.Empty;
            return guestName;
        }

        /// <summary>
        /// The guest visits the information booth.
        /// </summary>
        /// <param name="informationBooth">The information booth the guest is visiting.</param>
        public void VisitInformationBooth(GivingBooth informationBooth)
        {
            // Adds a map to the guest's bag.
            this.bag.Add(informationBooth.GiveFreeMap());

            // Adds a coupon book to the guest's bag.
            this.bag.Add(informationBooth.GiveFreeCouponBook());
        }

        /// <summary>
        /// The guest visits the ticket booth.
        /// </summary>
        /// <param name="ticketBooth">The ticket booth the guest is visiting.</param>
        /// <returns>Returns a ticket to the guest.</returns>
        public Ticket VisitTicketBoot(MoneyCollectingBooth ticketBooth)
        {
            if (this.wallet.MoneyBalance < ticketBooth.TicketPrice + ticketBooth.WaterBottlePrice)
            {
                this.WithdrawMoney(ticketBooth.TicketPrice * 2);
            }

            decimal amountAfterTicket = this.wallet.RemoveMoney(ticketBooth.TicketPrice);

            decimal amountAfterBottle = this.wallet.RemoveMoney(ticketBooth.WaterBottlePrice);

            // Adds a water bottle to the guests bag.
            this.bag.Add(ticketBooth.SellWaterBottle(amountAfterBottle));

            return ticketBooth.SellTicket(amountAfterTicket);
        }

        /// <summary>
        /// Withdraws money from the guest's checking account.
        /// </summary>
        /// <param name="amount">The amount to be withdrawn.</param>
        public void WithdrawMoney(decimal amount)
        {
            this.checkingAccount.RemoveMoney(amount);
            this.wallet.AddMoney(amount);
        }

        private void HandleBalanceChange()
        {
            if (this.OnTextChange != null)
            {
                this.OnTextChange(this);
            }
        }

        private void CreateTimers()
        {
            this.feedTimer = new Timer(5000);
            this.feedTimer.Elapsed += this.HandleReadyToFeed;
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }
    }
}
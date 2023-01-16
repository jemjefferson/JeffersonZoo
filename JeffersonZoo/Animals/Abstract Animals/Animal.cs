using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Timers;
using CagedItems;
using Foods;
using Reproducers;
using Utilities;

namespace Animals
{
    /// <summary>
    /// The class which is used to represent an animal.
    /// </summary>
    [Serializable]
    public abstract class Animal : IEater, IMover, IReproducer, ICageable
    {
        private static Random random = new Random(DateTime.Now.Millisecond);

        /// <summary>
        /// The age of the animal.
        /// </summary>
        private int age;

        /// <summary>
        /// The weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        private double babyWeightPercentage;

        /// <summary>
        /// A value indicating whether or not the animal is pregnant.
        /// </summary>
        private bool isPregnant;

        /// <summary>
        /// The name of the animal.
        /// </summary>
        private string name;

        /// <summary>
        /// The weight of the animal (in pounds).
        /// </summary>
        private double weight;

        /// <summary>
        /// The animal's gender.
        /// </summary>
        private Gender gender;

        [NonSerialized]
        private Timer moveTimer;

        [NonSerialized]
        private Action<Animal> onTextChange;

        private List<Animal> children;

        [NonSerialized]
        private Timer hungerTimer;

        /// <summary>
        /// Initializes a new instance of the Animal class.
        /// </summary>
        /// <param name="name">The name of the animal.</param>
        /// <param name="age">The age of the animal.</param>
        /// <param name="weight">The weight of the animal (in pounds).</param>
        /// <param name="gender">The animal's gender.</param>
        public Animal(string name, int age, double weight, Gender gender)
        {
            this.age = age;
            this.name = name;
            this.weight = weight;
            this.gender = gender;

            this.CreateTimers();

            this.XPositionMax = 800;
            this.YPositionMax = 400;

            this.MoveDistance = random.Next(5, 16);
            this.XPosition = random.Next(1, 801);
            this.YPosition = random.Next(1, 401);

            this.XDirection = (random.Next(0, 2) == 0) ? HorizontalDirection.Left : HorizontalDirection.Right;
            this.YDirection = (random.Next(0, 2) == 0) ? VerticalDirection.Up : VerticalDirection.Down;

            this.children = new List<Animal>();

            this.HungerState = HungerState.Satisfied;
        }

        /// <summary>
        /// gets or sets an alert to the zoo that an animal has become pregnant.
        /// </summary>
        public Action<IReproducer> OnPregnant { get; set; }

        /// <summary>
        /// Gets or sets how many pixels in the window the animal move.
        /// </summary>
        public int MoveDistance { get; set; }

        /// <summary>
        /// Gets or sets the way in which the animal will be facing (left or right/up or down).
        /// </summary>
        public HorizontalDirection XDirection { get; set; }

        /// <summary>
        /// Gets or sets the pixel coordinates (where in the cage) that the animal we be drawn at.
        /// </summary>
        public int XPosition { get; set; }

        /// <summary>
        /// Gets or sets the bounds of the window so that the animal stays within the window (cage).
        /// </summary>
        public int XPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the way in which the animal will be facing (left or right/up or down).
        /// </summary>
        public VerticalDirection YDirection { get; set; }

        /// <summary>
        /// Gets or sets the pixel coordinates (where in the cage) that the animal we be drawn at.
        /// </summary>
        public int YPosition { get; set; }

        /// <summary>
        /// Gets or sets the bounds of the window so that the animal stays within the window (cage).
        /// </summary>
        public int YPositionMax { get; set; }

        /// <summary>
        /// Gets or sets the hungerstate of the animal.
        /// </summary>
        public HungerState HungerState { get; set; }

        /// <summary>
        /// Gets or sets the onhunger action.
        /// </summary>
        public Action OnHunger { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the animal is active or not.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return this.moveTimer.Enabled;
            }

            set
            {
                this.moveTimer.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets the OnTextChange Action.
        /// </summary>
        public Action<Animal> OnTextChange
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
        /// Gets or sets the animal's age.
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                if (value >= 0 && value <= 100)
                {
                    this.age = value;
                    this.OnTextChange?.Invoke(this);
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Age must be set between 0 and 100.");
                }
            }
        }

        /// <summary>
        /// Gets the list of children animals.
        /// </summary>
        public IEnumerable<Animal> Children
        {
            get
            {
                return this.children;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether or not the animal is pregnant.
        /// </summary>
        public bool IsPregnant
        {
            get
            {
                return this.isPregnant;
            }

            set
            {
                this.isPregnant = value;
                this.OnTextChange?.Invoke(this);
            }
        }

        /// <summary>
        /// Gets or sets the animal's gender.
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
        /// Gets or sets the animal's movebehavior.
        /// </summary>
        public IMoveBehavior MoveBehavior { get; set; }

        /// <summary>
        /// Gets or sets the name of the animal.
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
                    this.OnTextChange?.Invoke(this);
                }
                else
                {
                    throw new FormatException("Name must only include letters and spaces.");
                }
            }
        }

        /// <summary>
        /// Gets or sets the animal's weight (in pounds).
        /// </summary>
        public double Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value >= 0 && value <= 1000)
                {
                    this.weight = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Weight must be between 0 and 1000");
                }
            }
        }

        /// <summary>
        /// Gets the percentage of weight gained for each pound of food eaten.
        /// </summary>
        public abstract double WeightGainPercentage
        {
            get;
        }

        /// <summary>
        /// Gets the display size of the animal.
        /// </summary>
        public virtual double DisplaySize
        {
            get
            {
                double displaySize = 0;
                displaySize = (this.Age == 0) ? 0.5 : 1.0;
                return displaySize;
            }
        }

        /// <summary>
        /// Gets the Resouce key of the animal.
        /// </summary>
        public string ResourceKey
        {
            get
            {
                string key = this.GetType().Name;
                key = (this.Age == 0) ? key + "Baby" : key + "Adult";
                return key;
            }
        }

        /// <summary>
        /// Gets or sets the eating behavior of the animal.
        /// </summary>
        public IEatBehavior EatBehavior { get; set; }

        /// <summary>
        /// Gets or sets the reproducing behavior of the animal.
        /// </summary>
        public IReproduceBehavior ReproduceBehavior { get; set; }

        /// <summary>
        /// Gets or sets the weight of a newborn baby (as a percentage of the parent's weight).
        /// </summary>
        public double BabyWeightPercentage
        {
            get
            {
                return this.babyWeightPercentage;
            }

            set
            {
                this.babyWeightPercentage = value;
            }
        }

        /// <summary>
        /// Gets or sets the on image update.
        /// </summary>
        public Action<ICageable> OnImageUpdate { get; set; }

        /// <summary>
        /// This method converts the animal to type based on the animal's type that is passed in.
        /// </summary>
        /// <param name="animalType">The animal's type.</param>
        /// <returns>Type that matches the animal type.</returns>
        public static Type ConvertAnimalToType(AnimalType animalType)
        {
            Type result = null;

            switch (animalType)
            {
                case AnimalType.Chimpanzee:
                    result = typeof(Chimpanzee);
                    break;

                case AnimalType.Dingo:
                    result = typeof(Dingo);
                    break;

                case AnimalType.Eagle:
                    result = typeof(Eagle);
                    break;

                case AnimalType.Hummingbird:
                    result = typeof(Hummingbird);
                    break;

                case AnimalType.Kangaroo:
                    result = typeof(Kangaroo);
                    break;

                case AnimalType.Ostrich:
                    result = typeof(Ostrich);
                    break;

                case AnimalType.Platypus:
                    result = typeof(Platypus);
                    break;

                case AnimalType.Shark:
                    result = typeof(Shark);
                    break;

                case AnimalType.Squirrel:
                    result = typeof(Squirrel);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Adds a child animal to animal's list of children.
        /// </summary>
        /// <param name="animal">Child animal to be added.</param>
        public void AddChild(Animal animal)
        {
            if (animal != null)
            {
                this.children.Add(animal);
            }
        }

        /// <summary>
        /// Eats the specified food.
        /// </summary>
        /// <param name="food">The food to eat.</param>
        public virtual void Eat(Food food)
        {
            this.EatBehavior.Eat(this, food);

            this.hungerTimer.Stop();
            this.HungerState = HungerState.Satisfied;
            this.hungerTimer.Start();
        }

        /// <summary>
        /// Makes the animal pregnant.
        /// </summary>
        public void MakePregnant()
        {
            this.IsPregnant = true;
            this.OnPregnant?.Invoke(this);
            this.MoveBehavior = new NoMoveBehavior();
        }

        /// <summary>
        /// The animal moves.
        /// </summary>
        public void Move()
        {
            this.MoveBehavior.Move(this);

            if (this.OnImageUpdate != null)
            {
                this.OnImageUpdate(this);
            }
        }

        /// <summary>
        /// Creates another reproducer of its own type.
        /// </summary>
        /// <returns>The resulting baby reproducer.</returns>
        public IReproducer Reproduce()
        {
            Gender gender = (random.Next(0, 2) == 0) ? Gender.Male : Gender.Female;

            // Create a baby reproducer.
            Animal baby = Activator.CreateInstance(this.GetType(), string.Empty, 0, this.Weight * (this.BabyWeightPercentage / 100), gender) as Animal;

            this.ReproduceBehavior.Reproduce(this, baby);

            this.AddChild(baby);

            // Make mother not pregnant after giving birth.
            this.IsPregnant = false;

            return baby;
        }

        /// <summary>
        /// Generates a string representation of the animal.
        /// </summary>
        /// <returns>A string representation of the animal.</returns>
        public override string ToString()
        {
            string animalString = this.name + ": " + this.GetType().Name + " (" + this.age + ", " + this.Weight + ")";

            animalString = (this.IsPregnant == true) ? animalString + " P" : animalString + string.Empty;
            return animalString;
        }

        private void MoveHandler(object sender, ElapsedEventArgs e)
        {
            // #if DEBUG
            // this.moveTimer.Stop();
            // #endif
            this.Move();

            #if DEBUG
            this.moveTimer.Start();
            #endif
        }

        private void CreateTimers()
        {
            this.moveTimer = new Timer(100);
            this.moveTimer.Elapsed += this.MoveHandler;
            this.moveTimer.Start();

            this.hungerTimer = new Timer(random.Next(10, 21) * 1000);
            this.hungerTimer.Elapsed += this.HandleHungerStateChange;
            this.hungerTimer.Start();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            this.CreateTimers();
        }

        private void HandleHungerStateChange(object sender, ElapsedEventArgs e)
        {
            switch (this.HungerState)
            {
                case HungerState.Satisfied:
                    this.HungerState = HungerState.Hungry;
                    break;
                case HungerState.Hungry:
                    this.HungerState = HungerState.Starving;
                    break;
                case HungerState.Starving:
                    this.HungerState = HungerState.Tired;

                    if (this.OnHunger != null)
                    {
                        this.OnHunger();
                    }

                    break;
                default:
                    break;
            }
        }
    }
}
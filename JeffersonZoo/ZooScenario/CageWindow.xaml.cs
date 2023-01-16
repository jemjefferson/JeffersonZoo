using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Animals;
using CagedItems;
using Utilities;
using Zoos;

namespace ZooScenario
{
    /// <summary>
    /// Interaction logic for CageWindow.xaml.
    /// </summary>
    public partial class CageWindow : Window
    {
        private Cage cage;

        /// <summary>
        /// Initializes an instance of the CageWindow Window.
        /// </summary>
        /// <param name="cage">A cage holding specific animals.</param>
        public CageWindow(Cage cage)
        {
            this.cage = cage;
            this.cage.OnImageUpdate = item =>
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(delegate()
                    {
                        int zIndex = 0;

                        foreach (Viewbox v in this.cageGrid.Children)
                        {
                            if (v.Tag == item)
                            {
                                this.cageGrid.Children.Remove(v);
                                break;
                            }

                            zIndex++;
                        }

                        if (item.IsActive == true)
                        {
                            this.DrawItem(item, zIndex);
                        }
                    }));
                }
                catch (TaskCanceledException)
                {
                }
            };
            this.InitializeComponent();
        }

        private void DrawItem(ICageable item, int zIndex)
        {
            Viewbox viewbox = this.GetViewBox(800, 400, item.XPosition, item.YPosition, item.ResourceKey, item.DisplaySize);
            viewbox.HorizontalAlignment = HorizontalAlignment.Left;
            viewbox.VerticalAlignment = VerticalAlignment.Top;

            TransformGroup transformGroup = new TransformGroup();
            if (item.HungerState == HungerState.Tired)
            {
                SkewTransform tiredSkew = new SkewTransform();
                tiredSkew.AngleX = item.XDirection == HorizontalDirection.Left ? 30.0 : -30.0;
                transformGroup.Children.Add(tiredSkew);
                transformGroup.Children.Add(new ScaleTransform(0.75, 0.5));
            }

            // If the animal is moving to the left
            if (item.XDirection == HorizontalDirection.Left)
            {
                // Set the origin point of the transformation to the middle of the viewBox.
                viewbox.RenderTransformOrigin = new Point(0.5, 0.5);

                // Initialize a ScaleTransform instance.
                ScaleTransform flipTransform = new ScaleTransform();

                // Flip the viewBox horizontally so the animal faces to the left
                flipTransform.ScaleX = -1;

                // Apply the ScaleTransform to the viewBox
                transformGroup.Children.Add(flipTransform);
            }

            viewbox.RenderTransform = transformGroup;

            viewbox.Tag = item;

            this.cageGrid.Children.Insert(zIndex, viewbox);
        }

        private void DrawAllItems()
        {
            this.cageGrid.Children.Clear();

            int zIndex = 0;

            this.cage.CagedItems.ToList().ForEach(c => this.DrawItem(c, zIndex++));
        }

        private Viewbox GetViewBox(double maxXPosition, double maxYPosition, int xPosition, int yPosition, string resourceKey, double displayScale)
        {
            Canvas canvas = Application.Current.Resources[resourceKey] as Canvas;

            // Finished viewBox.
            Viewbox finishedViewBox = new Viewbox();

            // Gets image ratio.
            double imageRatio = canvas.Width / canvas.Height;

            // Sets width to a percent of the window size based on it's scale.
            double itemWidth = this.cageGrid.ActualWidth * 0.2 * displayScale;

            // Sets the height to the ratio of the width.
            double itemHeight = itemWidth / imageRatio;

            // Sets the width of the viewBox to the size of the canvas.
            finishedViewBox.Width = itemWidth;
            finishedViewBox.Height = itemHeight;

            // Sets the animals location on the screen.
            double xPercent = (this.cageGrid.ActualWidth - itemWidth) / maxXPosition;
            double yPercent = (this.cageGrid.ActualHeight - itemHeight) / maxYPosition;

            int posX = Convert.ToInt32(xPosition * xPercent);
            int posY = Convert.ToInt32(yPosition * yPercent);

            finishedViewBox.Margin = new Thickness(posX, posY, 0, 0);

            // Adds the canvas to the view box.
            finishedViewBox.Child = canvas;

            // Returns the finished viewBox.
            return finishedViewBox;
        }

        private void CageWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DrawAllItems();
        }

        private void RedrawHandler(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(delegate()
            {
                this.DrawAllItems();
            }));
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.cage.OnImageUpdate = null;
        }
    }
}

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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// Interaction logic for Joystick.xaml.
    /// </summary>
    public partial class Joystick : UserControl
    {
        // Size of canvas - height and width.
        private double canvasWidth, canvasHeight, rudder, elevator;
        private Point startPoint = new Point();
        // For animation.
        private readonly Storyboard centerKnob;
        public Joystick()
        {
            InitializeComponent();
            // Reset rudder, elevator and centerKnob.
            rudder = elevator = 0;
            centerKnob = Knob.Resources["CenterKnob"] as Storyboard;
        }
        // Property of the elevator.
        public double ElevatorValue
        {
            get { return (double)GetValue(ElevatorValueProperty); }
            set
            {
                // Update elevator only if the value has been changed.
                if (value != elevator)
                {
                    // Check if the value is out of range or not.
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < -1)
                    {
                        value = -1;
                    }
                    elevator = value;
                    // Set the ElevatorValueProperty with our value after we checked his range.
                    SetValue(ElevatorValueProperty, value);
                }
                
            }
        }
        // DependencyProperty of ElevatorValue.
        public static readonly DependencyProperty ElevatorValueProperty =
            DependencyProperty.Register("ElevatorValue", typeof(double), typeof(Joystick));
        public double RudderValue
        {
            get { return (double)GetValue(RudderValueProperty); }
            set
            {
                // Update rudder only if the value has been changed.
                if (value != rudder)
                {
                    // Check if the value is out of range or not.
                    if (value > 1)
                    {
                        value = 1;
                    }
                    else if (value < -1)
                    {
                        value = -1;
                    }
                    rudder = value;
                    // Set the RudderValueProperty with our value after we checked his range.
                    SetValue(RudderValueProperty, value);
                }
                
            }
        }
        // DependencyProperty of RudderValue.
        public static readonly DependencyProperty RudderValueProperty =
            DependencyProperty.Register("RudderValue", typeof(double), typeof(Joystick));

        private Point updateKnobPosition;

        public Point UpdateKnobPosition
        {
            get { return updateKnobPosition; }
            set 
            {
                double powOfXY = Math.Pow(value.X, 2) + Math.Pow(value.Y, 2);
                double powDiameterOfBase = Math.Pow(canvasWidth / 2, 2);
                // The coordinates are in or on the circle.
                if (powOfXY <= powDiameterOfBase)
                {
                    // This coordinates are good.
                    updateKnobPosition = new Point(value.X, value.Y);
                } else
                {
                    // Else powOfXY > powDiameterOfBase - thats mean that the coordinates are out of the circle.
                    Point intersection1;
                    Point intersection2;
                    // Find the intersections point.
                    FindLineCircleIntersections(canvasWidth / 2.0, new Point(-value.X, -value.Y), out intersection1, out intersection2);
                    double dist1 = Math.Sqrt(Math.Pow(-intersection1.X, 2) + Math.Pow(-intersection1.Y, 2));
                    double dist2 = Math.Sqrt(Math.Pow(-intersection2.X, 2) + Math.Pow(-intersection2.Y, 2));
                    // Checking which point is the closest.
                    if (dist1 < dist2) { updateKnobPosition = intersection1; }
                    updateKnobPosition = intersection2;
                }
            }
        }

        private void CenterKnob_Completed(object sender, EventArgs e)
        {
        }
        // This function update the appropriate variables when the MouseDown.
        private void Knob_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // If left button was clicked.
            if (e.ChangedButton == MouseButton.Left)
            {
                startPoint.X = e.GetPosition(this).X;
                startPoint.Y = e.GetPosition(this).Y;
                canvasWidth = BlackCircle.ActualWidth - KnobBase.ActualWidth / 2;
                canvasHeight = BlackCircle.ActualHeight - KnobBase.ActualHeight / 2;
                // Make sure that the KnobBase does not leave the Knob.
                Knob.CaptureMouse();
                centerKnob.Stop();
            }
        }
        // This function reset the appropriate variables when the MouseUp.
        private void Knob_MouseUp(object sender, MouseButtonEventArgs e)
        {
            RudderValue = 0;
            ElevatorValue = 0;
            centerKnob.Begin();
            Knob.ReleaseMouseCapture();
        }
        // This function update the appropriate variables when the MouseMove.
        private void Knob_MouseMove(object sender, MouseEventArgs e)
        {
            // If left button was clicked.
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // If the mouse is not captured then return.
                if (!Knob.IsMouseCaptured) return;
                Point newPos = e.GetPosition(Base);
                UpdateKnobPosition = new Point(newPos.X - startPoint.X, newPos.Y - startPoint.Y);
                // Normalize rudder and elevator.
                RudderValue = (UpdateKnobPosition.X / (canvasWidth / 2));
                ElevatorValue = (-UpdateKnobPosition.Y / (canvasWidth / 2));
                knobPosition.X = UpdateKnobPosition.X;
                knobPosition.Y = UpdateKnobPosition.Y;
            }
        }
        // This function reset the values.
        public void ResetValues()
        {
            knobPosition.X = 0;
            knobPosition.Y = 0;
            RudderValue = 0;
            ElevatorValue = 0;
            centerKnob.Begin();
            Knob.ReleaseMouseCapture();
        }
        // Find the points of intersection.
        private void FindLineCircleIntersections(double radius, Point point2, out Point intersection1, out Point intersection2)
        {
            double dx, dy, A, C, delta, t;
            dx = point2.X;
            dy = point2.Y;
            // Calculate A,C for line equation (there is no need to calculate B because it will always be zero).
            A = dx * dx + dy * dy;
            C = - radius * radius;
            // Delta for finding solutions.
            delta = - 4 * A * C;
            // Two solutions.
            t = ((Math.Sqrt(delta)) / (2 * A));
            intersection1 = new Point(t * dx, t * dy);
            t = ((-Math.Sqrt(delta)) / (2 * A));
            intersection2 = new Point(t * dx, t * dy);       
        }
    }
}

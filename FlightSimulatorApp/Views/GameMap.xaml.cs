using Microsoft.Maps.MapControl.WPF;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulator.Views
{
    /// <summary>
    /// GameMap class that contains the map and will show the airplane and the coordinates.
    /// </summary>
    public partial class GameMap : UserControl
    {
        // This boolean variable is used to let know if set the view of the map.
        private bool firstTime = true;
        public GameMap()
        {
            InitializeComponent();
        }
        private void Pin_LayoutUpdated(object sender, EventArgs e)
        {
            if (pin.Location != null)
            {
                double latitude = pin.Location.Latitude;
                double longtitude = pin.Location.Longitude;
                // If it is the first time then set view and PlainPosiotion.
                if (firstTime)
                {
                    map_of_simulator.SetView(new Location(latitude, longtitude), 10);
                    PlainPosition.X = 0;
                    PlainPosition.Y = 0;
                    firstTime = false;
                    return;
                }
            }
        }
    }
}

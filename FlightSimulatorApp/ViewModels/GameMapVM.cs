using FlightSimulator.Models;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// This class is ViewModel of GameMap class and inherit the AbstractViewModel.
    /// </summary>
    public class GameMapVM : AbstractViewModel
    {
        public GameMapVM(AbstractFlightSimulatorModel modelCreated) : base(modelCreated)
        {
            // Notify to view from model.
            model.GetVariablesFromSimulator().PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        // Returns the Location from model.
        public Location VM_Location
        {
            get { return model.GetVariablesFromSimulator().Location; }
        }
        // Returns string of Longitude that will contain his value only if it is in domain defintion.
        public string VM_LongitudeT
        {
            get
            {
                // Check if the value is out of range and then return appropirate message.
                if (model.GetVariablesFromSimulator().Longitude == 180 || model.GetVariablesFromSimulator().Longitude == -180)
                {
                    return "Longitude: Invalid Coordinate";
                }
                return "Longitude: " + String.Format("{0:0.000}", model.GetVariablesFromSimulator().Longitude);
            }
        }
        // Returns string of Latitude that will contain his value only if it is in domain defintion.
        public string VM_LatitudeT
        {
            get
            {
                // Check if the value is out of range and then return appropirate message.
                if (model.GetVariablesFromSimulator().Latitude == 90 || model.GetVariablesFromSimulator().Latitude == -90)
                {
                    return "Latitude: Invalid Coordinate";
                }
                return "Latitude: " + String.Format("{0:0.000}", model.GetVariablesFromSimulator().Latitude);
            }
        }
    }
}

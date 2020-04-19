using FlightSimulator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulator.ViewModels
{
    /// <summary>
    /// This class is ViewModel of Dashboard class and inherit the AbstractViewModel.
    /// </summary>
    public class DashboardVM : AbstractViewModel
    {
        public DashboardVM(AbstractFlightSimulatorModel modelCreated) : base(modelCreated)
        {
            // Notify to view from model.
            model.GetVariablesFromSimulator().PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        // Returns string of Indicated_heading_deg that will contain only 3 digits after decimal point.
        public string VM_Indicated_heading_deg
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Indicated_heading_deg); }
        }
        // Returns string of Gps_indicated_vertical_speed that will contain only 3 digits after decimal point.
        public string VM_Gps_indicated_vertical_speed
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Gps_indicated_vertical_speed); }
        }
        // Returns string of Gps_indicated_ground_speed_kt that will contain only 3 digits after decimal point.
        public string VM_Gps_indicated_ground_speed_kt
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Gps_indicated_ground_speed_kt); }
        }
        // Returns string of Airspeed_indicator_indicated_speed_kt that will contain only 3 digits after decimal point.
        public string VM_Airspeed_indicator_indicated_speed_kt
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Airspeed_indicator_indicated_speed_kt); }
        }
        // Returns string of Gps_indicated_altitude_ft that will contain only 3 digits after decimal point.
        public string VM_Gps_indicated_altitude_ft
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Gps_indicated_altitude_ft); }
        }
        // Returns string of Attitude_indicator_internal_roll_deg that will contain only 3 digits after decimal point.
        public string VM_Attitude_indicator_internal_roll_deg
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Attitude_indicator_internal_roll_deg); }
        }
        // Returns string of Attitude_indicator_internal_pitch_deg that will contain only 3 digits after decimal point.
        public string VM_Attitude_indicator_internal_pitch_deg
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Attitude_indicator_internal_pitch_deg); }
        }
        // Returns string of Altimeter_indicated_altitude_ft that will contain only 3 digits after decimal point.
        public string VM_Altimeter_indicated_altitude_ft
        {
            get { return String.Format("{0:0.000}", model.GetVariablesFromSimulator().Altimeter_indicated_altitude_ft); }
        }
    }
}

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
    /// This class is ViewModel of Wheel class and inherit the AbstractViewModel
    /// </summary>
    public class WheelVM : AbstractViewModel
    {
        public WheelVM(AbstractFlightSimulatorModel modelCreated) : base(modelCreated) { }
        // Set Rudder in our model with this value.
        public double VM_Rudder
        {
            set 
            {
                // Check if the model is connected.
                if (model.IsConnect())
                {
                    model.GetDataFromWheel().Rudder = value;
                }
            }
        }
        // Set Elevator in our model with this value.
        public double VM_Elevator
        {
            set
            {
                // Check if the model is connected.
                if (model.IsConnect())
                {
                    model.GetDataFromWheel().Elevator = value;
                }
            }
        }
        // Set Throttle in our model with this value.
        public double VM_Throttle
        {
            set
            {
                // Check if the model is connected.
                if (model.IsConnect())
                {
                    model.GetDataFromWheel().Throttle = value;
                }
            }
        }
        // Set Aileron in our model with this value.
        public double VM_Aileron
        {
            set
            {
                // Check if the model is connected.
                if (model.IsConnect())
                {
                    model.GetDataFromWheel().Aileron = value;
                }
            }
        }        
    }
}
